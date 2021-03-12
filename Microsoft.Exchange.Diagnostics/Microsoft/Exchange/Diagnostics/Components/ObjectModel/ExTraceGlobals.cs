using System;

namespace Microsoft.Exchange.Diagnostics.Components.ObjectModel
{
	// Token: 0x02000375 RID: 885
	public static class ExTraceGlobals
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x00054099 File Offset: 0x00052299
		public static Trace DataSourceInfoTracer
		{
			get
			{
				if (ExTraceGlobals.dataSourceInfoTracer == null)
				{
					ExTraceGlobals.dataSourceInfoTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.dataSourceInfoTracer;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x000540B7 File Offset: 0x000522B7
		public static Trace DataSourceManagerTracer
		{
			get
			{
				if (ExTraceGlobals.dataSourceManagerTracer == null)
				{
					ExTraceGlobals.dataSourceManagerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.dataSourceManagerTracer;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x000540D5 File Offset: 0x000522D5
		public static Trace DataSourceSessionTracer
		{
			get
			{
				if (ExTraceGlobals.dataSourceSessionTracer == null)
				{
					ExTraceGlobals.dataSourceSessionTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.dataSourceSessionTracer;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x000540F3 File Offset: 0x000522F3
		public static Trace FieldTracer
		{
			get
			{
				if (ExTraceGlobals.fieldTracer == null)
				{
					ExTraceGlobals.fieldTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.fieldTracer;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00054111 File Offset: 0x00052311
		public static Trace ConfigObjectTracer
		{
			get
			{
				if (ExTraceGlobals.configObjectTracer == null)
				{
					ExTraceGlobals.configObjectTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.configObjectTracer;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0005412F File Offset: 0x0005232F
		public static Trace PropertyBagTracer
		{
			get
			{
				if (ExTraceGlobals.propertyBagTracer == null)
				{
					ExTraceGlobals.propertyBagTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.propertyBagTracer;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0005414D File Offset: 0x0005234D
		public static Trace QueryParserTracer
		{
			get
			{
				if (ExTraceGlobals.queryParserTracer == null)
				{
					ExTraceGlobals.queryParserTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.queryParserTracer;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0005416B File Offset: 0x0005236B
		public static Trace SchemaManagerTracer
		{
			get
			{
				if (ExTraceGlobals.schemaManagerTracer == null)
				{
					ExTraceGlobals.schemaManagerTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.schemaManagerTracer;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00054189 File Offset: 0x00052389
		public static Trace SecurityMangerTracer
		{
			get
			{
				if (ExTraceGlobals.securityMangerTracer == null)
				{
					ExTraceGlobals.securityMangerTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.securityMangerTracer;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x000541A7 File Offset: 0x000523A7
		public static Trace ConfigObjectReaderTracer
		{
			get
			{
				if (ExTraceGlobals.configObjectReaderTracer == null)
				{
					ExTraceGlobals.configObjectReaderTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.configObjectReaderTracer;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x000541C6 File Offset: 0x000523C6
		public static Trace IdentityTracer
		{
			get
			{
				if (ExTraceGlobals.identityTracer == null)
				{
					ExTraceGlobals.identityTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.identityTracer;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x000541E5 File Offset: 0x000523E5
		public static Trace RoleBasedStringMappingTracer
		{
			get
			{
				if (ExTraceGlobals.roleBasedStringMappingTracer == null)
				{
					ExTraceGlobals.roleBasedStringMappingTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.roleBasedStringMappingTracer;
			}
		}

		// Token: 0x0400195A RID: 6490
		private static Guid componentGuid = new Guid("b643f45b-9d4a-4186-ba92-05d5c229d692");

		// Token: 0x0400195B RID: 6491
		private static Trace dataSourceInfoTracer = null;

		// Token: 0x0400195C RID: 6492
		private static Trace dataSourceManagerTracer = null;

		// Token: 0x0400195D RID: 6493
		private static Trace dataSourceSessionTracer = null;

		// Token: 0x0400195E RID: 6494
		private static Trace fieldTracer = null;

		// Token: 0x0400195F RID: 6495
		private static Trace configObjectTracer = null;

		// Token: 0x04001960 RID: 6496
		private static Trace propertyBagTracer = null;

		// Token: 0x04001961 RID: 6497
		private static Trace queryParserTracer = null;

		// Token: 0x04001962 RID: 6498
		private static Trace schemaManagerTracer = null;

		// Token: 0x04001963 RID: 6499
		private static Trace securityMangerTracer = null;

		// Token: 0x04001964 RID: 6500
		private static Trace configObjectReaderTracer = null;

		// Token: 0x04001965 RID: 6501
		private static Trace identityTracer = null;

		// Token: 0x04001966 RID: 6502
		private static Trace roleBasedStringMappingTracer = null;
	}
}
