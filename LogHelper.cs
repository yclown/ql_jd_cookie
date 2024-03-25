using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
[assembly: log4net.Config.XmlConfigurator()]

namespace JD_Get
{
    
    public class LogHelper
    {
         
        static readonly ILog _logger = LogManager.GetLogger("LogTrace");
        public static void Info(string message)
        {
            _logger.Info(message);                               //打印事件
        }

        public static void Debug(string message)
        {
            _logger.Debug(message);                             //调试
        }

        public static void Warn(string message)
        {
            _logger.Warn(message);                              //警告
        }

        public static void Error(string message)
        {
            _logger.Error(message);                             //错误
        }
        public static void Error(Exception ex, string memo = "")
        {
            _logger.Error(FormatExceptionDetails(ex, memo));
        }
        /// <summary>
        /// 格式化异常信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string FormatExceptionDetails(Exception ex,string memo="")
        {
            StringBuilder sb = new StringBuilder();

            // 异常基本信息
            sb.AppendLine(memo+"Exception Details:");
            sb.AppendLine($"Exception Type: {ex.GetType().FullName}");
            sb.AppendLine($"Message: {ex.Message}");
            if (!string.IsNullOrEmpty(ex.Source))
                sb.AppendLine($"Source: {ex.Source}");

            // 堆栈跟踪
            sb.AppendLine("Stack Trace:");
            sb.AppendLine(ex.StackTrace);

            // 内部异常
            Exception innerEx = ex.InnerException;
            while (innerEx != null)
            {
                sb.AppendLine();
                sb.AppendLine("Inner Exception:");
                sb.AppendLine($"Exception Type: {innerEx.GetType().FullName}");
                sb.AppendLine($"Message: {innerEx.Message}");
                if (!string.IsNullOrEmpty(innerEx.Source))
                    sb.AppendLine($"Source: {innerEx.Source}");
                sb.AppendLine(innerEx.StackTrace);
                innerEx = innerEx.InnerException;
            }

            // （可选）添加自定义上下文信息，如当前时间、线程ID等
            sb.AppendLine();
            sb.AppendLine($"Time: {DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC")}");
            sb.AppendLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");

            return sb.ToString();
        }
    }
}
