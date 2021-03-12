using System;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000081 RID: 129
	internal sealed class DiagnosticTraceLogging : SystemTrace
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x0000A3F4 File Offset: 0x000085F4
		private DiagnosticTraceLogging(string assemblyName, int traceTag, string typeName, string traceSourceName) : base(assemblyName)
		{
			this.traceTag = traceTag;
			this.typeName = typeName;
			this.traceSourceName = traceSourceName;
			this.listener = new ExTraceListener(this.traceTag, this.traceSourceName + "_Trace");
			base.Initialize();
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000A445 File Offset: 0x00008645
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000A44D File Offset: 0x0000864D
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

		// Token: 0x060002E9 RID: 745 RVA: 0x0000A45C File Offset: 0x0000865C
		protected override void Update()
		{
			base.ConnectListener(this.source, this.listener, this.enabled);
			SourceLevels sourceLevels = this.enabled ? base.SourceLevels : SourceLevels.Off;
			if (this.propertyLevel != null && this.methodUpdateLevel != null)
			{
				this.propertyLevel.SetValue(null, sourceLevels, null);
				this.methodUpdateLevel.Invoke(null, null);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000A4D0 File Offset: 0x000086D0
		protected override bool Initialize(Assembly assembly)
		{
			Type type = assembly.GetType(this.typeName);
			if (type == null)
			{
				this.ReportFailure(string.Concat(new string[]
				{
					"type '",
					this.typeName,
					"' not found in assembly '",
					assembly.FullName,
					"'"
				}));
				return false;
			}
			TypeInfo typeInfo = type.GetTypeInfo();
			MethodInfo declaredMethod = typeInfo.GetDeclaredMethod("InitDiagnosticTraceImpl");
			if (declaredMethod == null || declaredMethod.IsPublic || !declaredMethod.IsStatic)
			{
				this.ReportFailure("static method 'InitDiagnosticTraceImpl' not found in type '" + type.FullName);
				return false;
			}
			declaredMethod.Invoke(null, new object[]
			{
				0,
				this.traceSourceName
			});
			this.methodUpdateLevel = typeInfo.GetDeclaredMethod("UpdateLevel");
			if (this.methodUpdateLevel == null || this.methodUpdateLevel.IsPublic || !this.methodUpdateLevel.IsStatic)
			{
				this.ReportFailure("static method 'UpdateLevel' not found in type '" + type.FullName + "'");
				return false;
			}
			this.propertyLevel = typeInfo.GetDeclaredProperty("Level");
			if (this.propertyLevel == null || this.propertyLevel.GetMethod == null || !this.propertyLevel.GetMethod.IsStatic)
			{
				this.ReportFailure("cannot find static property 'Level' in type '" + type.FullName + "'");
				return false;
			}
			PropertyInfo declaredProperty = typeInfo.GetDeclaredProperty("DiagnosticTrace");
			if (declaredProperty == null)
			{
				this.ReportFailure("static property 'DiagnosticTrace' not found in type '" + type.FullName + "'");
				return false;
			}
			object value = declaredProperty.GetValue(null, null);
			if (value == null)
			{
				this.ReportFailure("static property 'DiagnosticTrace' has null value");
				return false;
			}
			PropertyInfo propertyInfo = null;
			Type type2 = value.GetType();
			while (type2 != null && type2 != typeof(object))
			{
				TypeInfo typeInfo2 = type2.GetTypeInfo();
				propertyInfo = typeInfo2.GetDeclaredProperty("TraceSource");
				if (propertyInfo != null)
				{
					break;
				}
				type2 = typeInfo2.BaseType;
			}
			if (propertyInfo == null || propertyInfo.GetMethod == null)
			{
				this.ReportFailure("readable instance property 'TraceSource' not found in '" + type2.FullName + "' type");
				return false;
			}
			this.source = (propertyInfo.GetValue(value, null) as TraceSource);
			if (this.source == null)
			{
				this.ReportFailure("instance property 'TraceSource' does not return object of 'TraceSource' type");
				return false;
			}
			return true;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000A760 File Offset: 0x00008960
		private void ReportFailure(string failure)
		{
			InternalBypassTrace.TracingConfigurationTracer.TraceError(0, (long)this.traceTag, "Unable to initialize due the following failure: {0}", new object[]
			{
				failure
			});
		}

		// Token: 0x040002A7 RID: 679
		public static readonly DiagnosticTraceLogging SystemIdentityModel = new DiagnosticTraceLogging("System.IdentityModel", 3, "System.IdentityModel.DiagnosticUtility", "System.IdentityModel");

		// Token: 0x040002A8 RID: 680
		public static readonly DiagnosticTraceLogging SystemServiceModel = new DiagnosticTraceLogging("System.ServiceModel", 4, "System.ServiceModel.DiagnosticUtility", "System.ServiceModel");

		// Token: 0x040002A9 RID: 681
		private int traceTag;

		// Token: 0x040002AA RID: 682
		private string typeName;

		// Token: 0x040002AB RID: 683
		private string traceSourceName;

		// Token: 0x040002AC RID: 684
		private ExTraceListener listener;

		// Token: 0x040002AD RID: 685
		private MethodInfo methodUpdateLevel;

		// Token: 0x040002AE RID: 686
		private PropertyInfo propertyLevel;

		// Token: 0x040002AF RID: 687
		private bool enabled;

		// Token: 0x040002B0 RID: 688
		private TraceSource source;
	}
}
