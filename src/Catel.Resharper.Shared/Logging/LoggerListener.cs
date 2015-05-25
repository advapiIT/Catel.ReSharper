// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerListener.cs" company="Catel development team">
//   Copyright (c) 2008 - 2012 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Catel.ReSharper
{
    using Catel.Logging;
    using JetBrains.Util;

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
        

        public override void Debug(ILog log, string message)
        {
            Logger.LogMessage(message);
        }

        public override void Error(ILog log, string message)
        {
            Logger.LogError(message);
        }

        public override void Info(ILog log, string message)
        {
            Logger.LogMessage(message);
        }

        public override void Warning(ILog log, string message)
        {
            Logger.LogMessage(message);
        }

        #endregion
    }
}