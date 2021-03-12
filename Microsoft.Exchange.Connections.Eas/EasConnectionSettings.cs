using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class EasConnectionSettings
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000024E4 File Offset: 0x000006E4
		internal EasConnectionSettings(EasEndpointSettings easEndpointSettings, EasConnectionParameters connectionParameters, EasAuthenticationParameters authenticationParameters, EasDeviceParameters deviceParameters)
		{
			this.EasEndpointSettings = easEndpointSettings;
			this.ConnectionParameters = connectionParameters;
			this.AuthenticationParameters = authenticationParameters;
			this.DeviceParameters = deviceParameters;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002509 File Offset: 0x00000709
		internal ILog Log
		{
			get
			{
				return this.ConnectionParameters.Log;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002516 File Offset: 0x00000716
		internal EasProtocolVersion EasProtocolVersion
		{
			get
			{
				return this.ConnectionParameters.EasProtocolVersion;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002523 File Offset: 0x00000723
		internal string ClientLanguage
		{
			get
			{
				return this.ConnectionParameters.ClientLanguage;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002530 File Offset: 0x00000730
		internal bool RequestCompression
		{
			get
			{
				return this.ConnectionParameters.RequestCompression;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000253D File Offset: 0x0000073D
		internal bool AcceptMultipart
		{
			get
			{
				return this.ConnectionParameters.AcceptMultipart;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000254A File Offset: 0x0000074A
		internal string DeviceId
		{
			get
			{
				return this.DeviceParameters.DeviceId;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002557 File Offset: 0x00000757
		internal string DeviceType
		{
			get
			{
				return this.DeviceParameters.DeviceType;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002564 File Offset: 0x00000764
		internal string UserAgent
		{
			get
			{
				return this.DeviceParameters.UserAgent;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002571 File Offset: 0x00000771
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002579 File Offset: 0x00000779
		internal EasExtensionCapabilities ExtensionCapabilities { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002582 File Offset: 0x00000782
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000258A File Offset: 0x0000078A
		internal EasEndpointSettings EasEndpointSettings { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002593 File Offset: 0x00000793
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000259B File Offset: 0x0000079B
		private EasConnectionParameters ConnectionParameters { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000025A4 File Offset: 0x000007A4
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000025AC File Offset: 0x000007AC
		private EasDeviceParameters DeviceParameters { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000025B5 File Offset: 0x000007B5
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000025BD File Offset: 0x000007BD
		private EasAuthenticationParameters AuthenticationParameters { get; set; }
	}
}
