using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.Autodiscover;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x0200001D RID: 29
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlRoot(ElementName = "Autodiscover", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006")]
	public class AutodiscoverResponse : IHaveAnHttpStatus
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000038BD File Offset: 0x00001ABD
		// (set) Token: 0x060000AE RID: 174 RVA: 0x000038C5 File Offset: 0x00001AC5
		[XmlElement(ElementName = "Response", Namespace = "http://schemas.microsoft.com/exchange/autodiscover/mobilesync/responseschema/2006")]
		public Response Response { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000038CE File Offset: 0x00001ACE
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x000038D6 File Offset: 0x00001AD6
		[XmlIgnore]
		public HttpStatus HttpStatus { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000038DF File Offset: 0x00001ADF
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x000038E7 File Offset: 0x00001AE7
		[XmlIgnore]
		internal AutodiscoverStatus AutodiscoverStatus { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000038F0 File Offset: 0x00001AF0
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000038F8 File Offset: 0x00001AF8
		[XmlIgnore]
		internal string AutodiscoverSteps { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003904 File Offset: 0x00001B04
		[XmlIgnore]
		internal string AutodiscoveredDomain
		{
			get
			{
				if (this.Response == null || this.Response.Action == null || this.Response.Action.Settings == null || this.Response.Action.Settings.Server == null)
				{
					return string.Empty;
				}
				Server server = this.Response.Action.Settings.Server[0];
				string url = server.Url;
				Uri uri = new Uri(url);
				return uri.Host;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003985 File Offset: 0x00001B85
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000398C File Offset: 0x00001B8C
		private static Dictionary<byte, AutodiscoverStatus> StatusToEnumMap { get; set; } = new Dictionary<byte, AutodiscoverStatus>
		{
			{
				1,
				AutodiscoverStatus.Success
			},
			{
				2,
				AutodiscoverStatus.ProtocolError
			}
		};

		// Token: 0x060000B8 RID: 184 RVA: 0x00003994 File Offset: 0x00001B94
		internal void ConvertStatusToEnum()
		{
			byte b = (this.Response == null || this.Response.Error == null) ? 1 : ((byte)this.Response.Error.ErrorCode);
			this.AutodiscoverStatus = (AutodiscoverResponse.StatusToEnumMap.ContainsKey(b) ? AutodiscoverResponse.StatusToEnumMap[b] : ((AutodiscoverStatus)b));
		}
	}
}
