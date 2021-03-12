using System;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000AA RID: 170
	internal sealed class SystemServiceModelMessageLogging : SystemTrace
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x0000F580 File Offset: 0x0000D780
		private SystemServiceModelMessageLogging() : base("System.ServiceModel")
		{
			this.listener = new ExTraceListener(5, "System.ServiceModel_MessageLogging");
			base.Initialize();
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
				base.SafeUpdate();
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000F5BB File Offset: 0x0000D7BB
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000F5C3 File Offset: 0x0000D7C3
		public bool LogMalformedMessages
		{
			get
			{
				return this.logMalformedMessages;
			}
			set
			{
				this.logMalformedMessages = value;
				base.SafeUpdate();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000F5D2 File Offset: 0x0000D7D2
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000F5DA File Offset: 0x0000D7DA
		public bool LogMessagesAtServiceLevel
		{
			get
			{
				return this.logMessagesAtServiceLevel;
			}
			set
			{
				this.logMessagesAtServiceLevel = value;
				base.SafeUpdate();
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000F5E9 File Offset: 0x0000D7E9
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000F5F1 File Offset: 0x0000D7F1
		public bool LogMessagesAtTransportLevel
		{
			get
			{
				return this.logMessagesAtTransportLevel;
			}
			set
			{
				this.logMessagesAtTransportLevel = value;
				base.SafeUpdate();
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000F600 File Offset: 0x0000D800
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000F608 File Offset: 0x0000D808
		public bool LogMessageBody
		{
			get
			{
				return this.logMessageBody;
			}
			set
			{
				this.logMessageBody = value;
				base.SafeUpdate();
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000F618 File Offset: 0x0000D818
		protected override void Update()
		{
			bool flag = this.Enabled && (this.logMalformedMessages || this.logMessagesAtServiceLevel || this.logMessagesAtTransportLevel || this.logMessageBody);
			base.ConnectListener(this.source, this.listener, flag);
			this.source.Switch.Level = (flag ? base.SourceLevels : SourceLevels.Off);
			SystemTrace.SetPropertyValue(this.propertyLogMalformedMessages, this.logMalformedMessages);
			SystemTrace.SetPropertyValue(this.propertyLogMessagesAtServiceLevel, this.logMessagesAtServiceLevel);
			SystemTrace.SetPropertyValue(this.propertyLogMessagesAtTransportLevel, this.logMessagesAtTransportLevel);
			SystemTrace.SetPropertyValue(this.propertyLogMessageBody, this.logMessageBody);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		protected override bool Initialize(Assembly assembly)
		{
			Type type = assembly.GetType("System.ServiceModel.Diagnostics.MessageLogger");
			if (type == null)
			{
				SystemServiceModelMessageLogging.ReportFailure("type 'System.ServiceModel.Diagnostics.MessageLogger' not found in assembly " + assembly.FullName);
				return false;
			}
			FieldInfo declaredField = type.GetTypeInfo().GetDeclaredField("messageTraceSource");
			if (declaredField == null || declaredField.IsPublic || !declaredField.IsStatic)
			{
				SystemServiceModelMessageLogging.ReportFailure("field 'messageTraceSource' not found in type " + type.Name);
				return false;
			}
			Assembly assembly2 = SystemTrace.GetAssembly("SMDiagnostics");
			if (assembly2 == null)
			{
				SystemServiceModelMessageLogging.ReportFailure("assembly 'SMDiagnostics' not found");
				return false;
			}
			Type type2 = assembly2.GetType("System.ServiceModel.Diagnostics.PiiTraceSource");
			if (type2 == null)
			{
				SystemServiceModelMessageLogging.ReportFailure("type 'System.ServiceModel.Diagnostics.PiiTraceSource' not found in assembly " + assembly2.FullName);
				return false;
			}
			ConstructorInfo constructorInfo = null;
			foreach (ConstructorInfo constructorInfo2 in type2.GetTypeInfo().DeclaredConstructors)
			{
				if (!constructorInfo2.IsPublic)
				{
					ParameterInfo[] parameters = constructorInfo2.GetParameters();
					if (parameters != null && parameters.Length == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(string))
					{
						constructorInfo = constructorInfo2;
						break;
					}
				}
			}
			if (constructorInfo == null)
			{
				SystemServiceModelMessageLogging.ReportFailure("instance constructor 'System.ServiceModel.Diagnostics.PiiTraceSource(string,string)' not found");
				return false;
			}
			object obj = constructorInfo.Invoke(new object[]
			{
				"System.ServiceModel.MessageLogging",
				"System.ServiceModel 4.0.0.0"
			});
			this.source = (obj as TraceSource);
			if (this.source == null)
			{
				SystemServiceModelMessageLogging.ReportFailure("instance constructor 'System.ServiceModel.Diagnostics.PiiTraceSource(string,string)' did not return TraceSource object");
				return false;
			}
			this.source.Switch.Level = SourceLevels.Off;
			this.source.Listeners.Remove("Default");
			declaredField.SetValue(null, this.source);
			TypeInfo typeInfo = type.GetTypeInfo();
			this.propertyLogMalformedMessages = typeInfo.GetDeclaredProperty("LogMalformedMessages");
			this.propertyLogMessagesAtServiceLevel = typeInfo.GetDeclaredProperty("LogMessagesAtServiceLevel");
			this.propertyLogMessagesAtTransportLevel = typeInfo.GetDeclaredProperty("LogMessagesAtTransportLevel");
			this.propertyLogMessageBody = typeInfo.GetDeclaredProperty("LogMessageBody");
			SystemServiceModelMessageLogging.SetMinimum(type, "MaxMessageSize", 262144);
			SystemServiceModelMessageLogging.SetMinimum(type, "MaxNumberOfMessagesToLog", 10000);
			return true;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000F940 File Offset: 0x0000DB40
		private static void SetMinimum(Type type, string propertyName, int defaultValue)
		{
			PropertyInfo declaredProperty = type.GetTypeInfo().GetDeclaredProperty(propertyName);
			if (declaredProperty != null)
			{
				int num = (int)declaredProperty.GetValue(declaredProperty, null);
				if (num < defaultValue)
				{
					declaredProperty.SetValue(declaredProperty, defaultValue, null);
				}
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000F983 File Offset: 0x0000DB83
		private static void ReportFailure(string failure)
		{
			ExTraceInternal.Trace<string>(0, TraceType.ErrorTrace, SystemLoggingTags.guid, 5, 0L, "Unable to initialize due the following failure: {0}", failure);
		}

		// Token: 0x0400034E RID: 846
		public static readonly SystemServiceModelMessageLogging Instance = new SystemServiceModelMessageLogging();

		// Token: 0x0400034F RID: 847
		private PropertyInfo propertyLogMalformedMessages;

		// Token: 0x04000350 RID: 848
		private PropertyInfo propertyLogMessagesAtServiceLevel;

		// Token: 0x04000351 RID: 849
		private PropertyInfo propertyLogMessagesAtTransportLevel;

		// Token: 0x04000352 RID: 850
		private PropertyInfo propertyLogMessageBody;

		// Token: 0x04000353 RID: 851
		private TraceSource source;

		// Token: 0x04000354 RID: 852
		private ExTraceListener listener;

		// Token: 0x04000355 RID: 853
		private bool enabled;

		// Token: 0x04000356 RID: 854
		private bool logMalformedMessages;

		// Token: 0x04000357 RID: 855
		private bool logMessagesAtServiceLevel;

		// Token: 0x04000358 RID: 856
		private bool logMessagesAtTransportLevel;

		// Token: 0x04000359 RID: 857
		private bool logMessageBody;
	}
}
