// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerListener.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Catel.ReSharper
{
    using System;

    using Catel.Logging;
    using JetBrains.Util;
    using LogEvent = Logging.LogEvent;
#if R80 || R81 || R82 || R90
    using JetBrains.Util.Logging;
#endif

    /// <summary>
    /// The logger listener.
    /// </summary>
    public class LoggerListener : LogListenerBase
    {
        #region Constructors and Destructors
        public LoggerListener()
        {
#if DEBUG && !R90
            Logger.AppendListener(new DebugOutputLogEventListener("CatelR#"));
#endif
        }

        #endregion

        #region Public Methods and Operators
        protected override void Write(ILog log, string message, LogEvent logEvent, object extraData, LogData logData, DateTime time)
        {
            Logger.LogMessage(message);
        }

        #endregion
    }
}