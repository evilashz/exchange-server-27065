using System;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A9 RID: 169
	internal sealed class SystemNetLogging : SystemTrace
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x0000F15C File Offset: 0x0000D35C
		private SystemNetLogging() : base("System")
		{
			this.socketsListener = new ExTraceListener(1, "Sockets");
			this.webListener = new ExTraceListener(0, "Web");
			this.httpListenerListener = new ExTraceListener(2, "HttpListener");
			base.Initialize();
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000F1AD File Offset: 0x0000D3AD
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0000F1B5 File Offset: 0x0000D3B5
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

		// Token: 0x06000429 RID: 1065 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		internal void AddHttpListenerExtendedErrorListener(TraceListener extendedErrorListener)
		{
			lock (this)
			{
				if (this.httpListenerExtendedErrorListener != null)
				{
					throw new InvalidOperationException("Only a single extended error listener allowed");
				}
				this.httpListenerExtendedErrorListener = extendedErrorListener;
			}
			base.SafeUpdate();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000F21C File Offset: 0x0000D41C
		internal void RemoveHttpListenerExtendedErrorListener(TraceListener extendedErrorListener)
		{
			lock (this)
			{
				if (this.httpListenerExtendedErrorListener != null)
				{
					base.ConnectListener(this.httpListenerSource, this.httpListenerExtendedErrorListener, false);
					this.httpListenerExtendedErrorListener = null;
				}
			}
			base.SafeUpdate();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000F27C File Offset: 0x0000D47C
		protected override void Update()
		{
			bool flag = this.httpListenerExtendedErrorListener != null;
			SystemTrace.SetFieldValue(this.fieldEnabled, flag || this.enabled);
			SourceLevels sourceLevels = this.enabled ? base.SourceLevels : SourceLevels.Off;
			this.socketsSource.Switch.Level = sourceLevels;
			this.webSource.Switch.Level = sourceLevels;
			this.httpListenerSource.Switch.Level = ((flag && (sourceLevels == SourceLevels.Off || sourceLevels == SourceLevels.Critical)) ? SourceLevels.Error : sourceLevels);
			base.ConnectListener(this.socketsSource, this.socketsListener, this.enabled);
			base.ConnectListener(this.webSource, this.webListener, this.enabled);
			base.ConnectListener(this.httpListenerSource, this.httpListenerListener, this.enabled);
			if (flag)
			{
				base.ConnectListener(this.httpListenerSource, this.httpListenerExtendedErrorListener, true);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000F364 File Offset: 0x0000D564
		protected override bool Initialize(Assembly assembly)
		{
			Type type = assembly.GetType("System.Net.Logging");
			if (type == null)
			{
				string failure = "type 'System.Net.Logging' not found in assembly '" + assembly.FullName + "'";
				SystemNetLogging.ReportFailure(1, failure);
				SystemNetLogging.ReportFailure(0, failure);
				return false;
			}
			this.fieldEnabled = type.GetTypeInfo().GetDeclaredField("s_LoggingEnabled");
			if (this.fieldEnabled == null || this.fieldEnabled.IsPublic || !this.fieldEnabled.IsStatic)
			{
				string failure2 = "static field 's_LoggingEnabled' not found in type 'System.Net.Logging'";
				SystemNetLogging.ReportFailure(1, failure2);
				SystemNetLogging.ReportFailure(0, failure2);
				return false;
			}
			SystemTrace.SetFieldValue(this.fieldEnabled, false);
			PropertyInfo declaredProperty = type.GetTypeInfo().GetDeclaredProperty("On");
			if (declaredProperty == null)
			{
				string failure3 = "static property 'On' not found in 'System.Net.Logging' type";
				SystemNetLogging.ReportFailure(1, failure3);
				SystemNetLogging.ReportFailure(0, failure3);
				return false;
			}
			declaredProperty.GetValue(null, null);
			this.socketsSource = SystemNetLogging.GetTraceSource(1, "s_SocketsTraceSource", type);
			this.webSource = SystemNetLogging.GetTraceSource(0, "s_WebTraceSource", type);
			this.httpListenerSource = SystemNetLogging.GetTraceSource(2, "s_HttpListenerTraceSource", type);
			return this.webSource != null || this.socketsSource != null || this.httpListenerSource != null;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		private static TraceSource GetTraceSource(int traceTag, string fieldName, Type type)
		{
			FieldInfo declaredField = type.GetTypeInfo().GetDeclaredField(fieldName);
			if (declaredField == null || declaredField.IsPublic || !declaredField.IsStatic)
			{
				SystemNetLogging.ReportFailure(traceTag, string.Concat(new string[]
				{
					"static field '",
					fieldName,
					"' not found in '",
					type.Name,
					"' type"
				}));
				return null;
			}
			TraceSource traceSource = declaredField.GetValue(null) as TraceSource;
			if (traceSource == null)
			{
				SystemNetLogging.ReportFailure(traceTag, "static field '" + fieldName + "' does not contain object of 'TraceSource' type");
				return null;
			}
			traceSource.Listeners.Remove("Default");
			traceSource.Switch.Level = SourceLevels.Off;
			traceSource.SetMaxDataSize(1073741824);
			return traceSource;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000F55D File Offset: 0x0000D75D
		private static void ReportFailure(int traceTag, string failure)
		{
			ExTraceInternal.Trace<string>(0, TraceType.ErrorTrace, SystemLoggingTags.guid, traceTag, 0L, "Unable to initialize due the following failure: {0}", failure);
		}

		// Token: 0x04000343 RID: 835
		private const int MaxDataSize = 1073741824;

		// Token: 0x04000344 RID: 836
		public static readonly SystemNetLogging Instance = new SystemNetLogging();

		// Token: 0x04000345 RID: 837
		private bool enabled;

		// Token: 0x04000346 RID: 838
		private FieldInfo fieldEnabled;

		// Token: 0x04000347 RID: 839
		private TraceSource socketsSource;

		// Token: 0x04000348 RID: 840
		private TraceSource webSource;

		// Token: 0x04000349 RID: 841
		private TraceSource httpListenerSource;

		// Token: 0x0400034A RID: 842
		private ExTraceListener socketsListener;

		// Token: 0x0400034B RID: 843
		private ExTraceListener webListener;

		// Token: 0x0400034C RID: 844
		private ExTraceListener httpListenerListener;

		// Token: 0x0400034D RID: 845
		private TraceListener httpListenerExtendedErrorListener;
	}
}
