using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using Accidis.Apps.Preflight.Utils.Diagnostics;

namespace Accidis.Apps.Preflight.Utils.Web
{
	public abstract class WebDownloaderBase<TResult> where TResult : new()
	{
		const int _defaultTimeout = 30000;

		protected virtual int Timeout
		{
			get { return _defaultTimeout; }
		}

		protected virtual bool CloseResponse
		{
			get { return true; }
		}

		public event EventHandler DownloadStarted;
		public event EventHandler<UnhandledExceptionEventArgs> DownloadFailed;
		public event EventHandler DownloadCompleted;

		protected abstract void ReadResponse( Stream stream, TResult result );

		protected virtual void AfterDownloadCompleted( TResult result )
		{
		}

		protected void BeginDownload( string url )
		{
			if( null != DownloadStarted )
				DownloadStarted( this, EventArgs.Empty );

			var state = new RequestState
			            	{
			            		Timer = new LoggingTimer( "WebDownloaderBase.BeginDownload" ),
			            		Result = new TResult(),
			            		Request = WebRequest.CreateHttp( url )
			            	};

			state.Request.AllowReadStreamBuffering = true;
			state.Request.BeginGetResponse( OnWebRequestCompleted, state );
			ThreadPool.QueueUserWorkItem( HandleTimeout, state );
		}

		void HandleTimeout( object stateObj )
		{
			Thread.Sleep( Timeout );

			var state = (RequestState) stateObj;
			if( !state.Completed )
			{
				Debug.WriteLine( "Web request timed out!" );
				state.Request.Abort();
			}
		}

		void OnWebRequestCompleted( IAsyncResult asyncResult )
		{
			var state = (RequestState) asyncResult.AsyncState;
			state.Timer.Dispose();
			state.Completed = true;

			WebResponse response = null;

			try
			{
				using( new LoggingTimer( "WebDownloaderBase.OnWebRequestCompleted" ) )
				{
					response = state.Request.EndGetResponse( asyncResult );
					ReadResponse( response.GetResponseStream(), state.Result );
				}
			}
			catch( Exception ex )
			{
				Debug.WriteLine( "Exception during download of {0}", state.Request.RequestUri );
				Debug.WriteLine( ex );

				if( null != DownloadFailed )
					DownloadFailed( this, new UnhandledExceptionEventArgs( ex, false ) );

				return;
			}
			finally
			{
				if( CloseResponse && null != response )
					response.Close();
            }

            AfterDownloadCompleted( state.Result );

			if( null != DownloadCompleted )
				DownloadCompleted( this, EventArgs.Empty );
		}

		#region Nested type: RequestState

		sealed class RequestState
		{
			public HttpWebRequest Request { get; set; }
			public LoggingTimer Timer { get; set; }
			public TResult Result { get; set; }
			public bool Completed { get; set; }
		}

		#endregion
	}
}