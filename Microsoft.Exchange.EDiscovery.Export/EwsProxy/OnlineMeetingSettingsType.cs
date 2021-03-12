using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000149 RID: 329
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class OnlineMeetingSettingsType
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000228B0 File Offset: 0x00020AB0
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x000228B8 File Offset: 0x00020AB8
		public LobbyBypassType LobbyBypass
		{
			get
			{
				return this.lobbyBypassField;
			}
			set
			{
				this.lobbyBypassField = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x000228C1 File Offset: 0x00020AC1
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x000228C9 File Offset: 0x00020AC9
		public OnlineMeetingAccessLevelType AccessLevel
		{
			get
			{
				return this.accessLevelField;
			}
			set
			{
				this.accessLevelField = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x000228D2 File Offset: 0x00020AD2
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x000228DA File Offset: 0x00020ADA
		public PresentersType Presenters
		{
			get
			{
				return this.presentersField;
			}
			set
			{
				this.presentersField = value;
			}
		}

		// Token: 0x040009DD RID: 2525
		private LobbyBypassType lobbyBypassField;

		// Token: 0x040009DE RID: 2526
		private OnlineMeetingAccessLevelType accessLevelField;

		// Token: 0x040009DF RID: 2527
		private PresentersType presentersField;
	}
}
