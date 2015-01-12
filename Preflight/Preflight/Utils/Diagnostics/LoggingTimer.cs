using System;
using System.Diagnostics;

namespace Accidis.Apps.Preflight.Utils.Diagnostics
{
	public sealed class LoggingTimer : IDisposable
	{
		readonly string _caption;
		readonly Logger _logger;
		Stopwatch _timer;

		public LoggingTimer( Logger logger, string caption )
		{
			_logger = logger;
			_caption = caption;
			StartTimer();
		}

		public LoggingTimer( string caption ) : this( null, caption )
		{
		}

		public void Dispose()
		{
			StopTimer();
		}

		[Conditional( "DEBUG" )]
		void StartTimer()
		{
			_timer = new Stopwatch();
			_timer.Start();
		}

		[Conditional( "DEBUG" )]
		void StopTimer()
		{
			_timer.Stop();
			string message = String.Format( "{0}: {1} ms", _caption, _timer.ElapsedMilliseconds );

			if( null != _logger )
				_logger.Debug( message );
			else
				Debug.WriteLine( message );
		}
	}
}