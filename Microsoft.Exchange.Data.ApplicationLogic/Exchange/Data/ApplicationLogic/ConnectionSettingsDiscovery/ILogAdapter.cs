using System;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery
{
	// Token: 0x020001A1 RID: 417
	internal interface ILogAdapter
	{
		// Token: 0x06000FBB RID: 4027
		void Trace(string messageTemplate, params object[] args);

		// Token: 0x06000FBC RID: 4028
		void LogError(string messageTemplate, params object[] args);

		// Token: 0x06000FBD RID: 4029
		void LogException(Exception exception, string additionalMessage, params object[] args);

		// Token: 0x06000FBE RID: 4030
		void ExecuteMonitoredOperation(Enum logMetadata, Action operation);

		// Token: 0x06000FBF RID: 4031
		void LogOperationResult(Enum logMetadata, string domain, bool succeeded);

		// Token: 0x06000FC0 RID: 4032
		void LogOperationException(Enum logMetadata, Exception ex);

		// Token: 0x06000FC1 RID: 4033
		void RegisterLogMetaData(string actionName, Type logMetaDataEnumType);
	}
}
