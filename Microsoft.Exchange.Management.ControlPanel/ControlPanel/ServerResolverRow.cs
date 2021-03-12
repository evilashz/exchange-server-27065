using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200010B RID: 267
	[DataContract]
	public class ServerResolverRow : AdObjectResolverRow
	{
		// Token: 0x06001F95 RID: 8085 RVA: 0x0005F1F6 File Offset: 0x0005D3F6
		public ServerResolverRow(ADRawEntry aDRawEntry) : base(aDRawEntry)
		{
			this.OperationalState = Strings.No;
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x0005F20F File Offset: 0x0005D40F
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x0005F226 File Offset: 0x0005D426
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base.ADRawEntry[ADObjectSchema.Name];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0005F22D File Offset: 0x0005D42D
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x0005F235 File Offset: 0x0005D435
		[DataMember]
		public string OperationalState { get; set; }

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0005F240 File Offset: 0x0005D440
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x0005F296 File Offset: 0x0005D496
		[DataMember]
		public string ServerRole
		{
			get
			{
				ServerRole serverRole = (ServerRole)base.ADRawEntry[ExchangeServerSchema.CurrentServerRole];
				if (!(bool)base.ADRawEntry[ExchangeServerSchema.IsE15OrLater])
				{
					return serverRole.ToString();
				}
				return ExchangeServer.ConvertE15ServerRoleToOutput(serverRole).ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x0005F29D File Offset: 0x0005D49D
		// (set) Token: 0x06001F9D RID: 8093 RVA: 0x0005F2B9 File Offset: 0x0005D4B9
		[DataMember]
		public string Site
		{
			get
			{
				return ((ADObjectId)base.ADRawEntry[ExchangeServerSchema.Site]).ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x0005F2C0 File Offset: 0x0005D4C0
		// (set) Token: 0x06001F9F RID: 8095 RVA: 0x0005F2E9 File Offset: 0x0005D4E9
		[DataMember]
		public string AdminDisplayVersion
		{
			get
			{
				ServerVersion serverVersion = (ServerVersion)base.ADRawEntry[ExchangeServerSchema.AdminDisplayVersion];
				return serverVersion.ToString();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04001C7E RID: 7294
		public new static PropertyDefinition[] Properties = new List<PropertyDefinition>(AdObjectResolverRow.Properties)
		{
			ADObjectSchema.Name,
			ExchangeServerSchema.CurrentServerRole,
			ExchangeServerSchema.IsE15OrLater,
			ExchangeServerSchema.Site,
			ExchangeServerSchema.AdminDisplayVersion
		}.ToArray();
	}
}
