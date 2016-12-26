﻿using System;

namespace Classifieds.Common
{
    public class Logger: ILogger
    {
        private Classifieds.NLog.MongoDB.ILogger _logger;
        public Logger()
        {
            _logger = new Classifieds.NLog.MongoDB.Logger();
        }

        public Exception Log(Exception ex, string userId)
        {
            _logger.Log(ex, userId);
            return ex;
        }
    }
}
