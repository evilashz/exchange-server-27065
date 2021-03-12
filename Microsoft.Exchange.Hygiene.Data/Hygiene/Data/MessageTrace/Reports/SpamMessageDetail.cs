using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports
{
	// Token: 0x02000199 RID: 409
	internal class SpamMessageDetail : Schema
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x00034EBC File Offset: 0x000330BC
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x00034ECE File Offset: 0x000330CE
		public string Organization
		{
			get
			{
				return (string)this[SpamMessageDetail.OrganizationDefinition];
			}
			set
			{
				this[SpamMessageDetail.OrganizationDefinition] = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00034EDC File Offset: 0x000330DC
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x00034EEE File Offset: 0x000330EE
		public string Domain
		{
			get
			{
				return (string)this[SpamMessageDetail.DomainDefinition];
			}
			set
			{
				this[SpamMessageDetail.DomainDefinition] = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00034EFC File Offset: 0x000330FC
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x00034F0E File Offset: 0x0003310E
		public DateTime Received
		{
			get
			{
				return (DateTime)this[SpamMessageDetail.ReceivedDefinition];
			}
			set
			{
				this[SpamMessageDetail.ReceivedDefinition] = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00034F21 File Offset: 0x00033121
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x00034F33 File Offset: 0x00033133
		public string ClientMessageId
		{
			get
			{
				return (string)this[SpamMessageDetail.ClientMessageIdDefinition];
			}
			set
			{
				this[SpamMessageDetail.ClientMessageIdDefinition] = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00034F41 File Offset: 0x00033141
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x00034F53 File Offset: 0x00033153
		public string Direction
		{
			get
			{
				return (string)this[SpamMessageDetail.DirectionDefinition];
			}
			set
			{
				this[SpamMessageDetail.DirectionDefinition] = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00034F61 File Offset: 0x00033161
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00034F73 File Offset: 0x00033173
		public string RecipientAddress
		{
			get
			{
				return (string)this[SpamMessageDetail.RecipientAddressDefinition];
			}
			set
			{
				this[SpamMessageDetail.RecipientAddressDefinition] = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00034F81 File Offset: 0x00033181
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x00034F93 File Offset: 0x00033193
		public string SenderAddress
		{
			get
			{
				return (string)this[SpamMessageDetail.SenderAddressDefinition];
			}
			set
			{
				this[SpamMessageDetail.SenderAddressDefinition] = value;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00034FA1 File Offset: 0x000331A1
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x00034FB3 File Offset: 0x000331B3
		public string MessageSubject
		{
			get
			{
				return (string)this[SpamMessageDetail.MessageSubjectDefinition];
			}
			set
			{
				this[SpamMessageDetail.MessageSubjectDefinition] = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00034FC1 File Offset: 0x000331C1
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x00034FD3 File Offset: 0x000331D3
		public int MessageSize
		{
			get
			{
				return (int)this[SpamMessageDetail.MessageSizeDefinition];
			}
			set
			{
				this[SpamMessageDetail.MessageSizeDefinition] = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x00034FE6 File Offset: 0x000331E6
		// (set) Token: 0x06001135 RID: 4405 RVA: 0x00034FF8 File Offset: 0x000331F8
		public string EventType
		{
			get
			{
				return (string)this[SpamMessageDetail.EventTypeDefinition];
			}
			set
			{
				this[SpamMessageDetail.EventTypeDefinition] = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00035006 File Offset: 0x00033206
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x00035018 File Offset: 0x00033218
		public string Action
		{
			get
			{
				return (string)this[SpamMessageDetail.ActionDefinition];
			}
			set
			{
				this[SpamMessageDetail.ActionDefinition] = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00035026 File Offset: 0x00033226
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x00035038 File Offset: 0x00033238
		public Guid InternalMessageId
		{
			get
			{
				return (Guid)this[SpamMessageDetail.InternalMessageIdDefinition];
			}
			set
			{
				this[SpamMessageDetail.InternalMessageIdDefinition] = value;
			}
		}

		// Token: 0x0400082D RID: 2093
		internal static readonly HygienePropertyDefinition OrganizationDefinition = new HygienePropertyDefinition("OrganizationalUnitRootId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400082E RID: 2094
		internal static readonly HygienePropertyDefinition DomainDefinition = new HygienePropertyDefinition("DomainName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400082F RID: 2095
		internal static readonly HygienePropertyDefinition ReceivedDefinition = new HygienePropertyDefinition("Received", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000830 RID: 2096
		internal static readonly HygienePropertyDefinition ClientMessageIdDefinition = new HygienePropertyDefinition("ClientMessageId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000831 RID: 2097
		internal static readonly HygienePropertyDefinition DirectionDefinition = new HygienePropertyDefinition("Direction", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000832 RID: 2098
		internal static readonly HygienePropertyDefinition RecipientAddressDefinition = new HygienePropertyDefinition("RecipientAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000833 RID: 2099
		internal static readonly HygienePropertyDefinition SenderAddressDefinition = new HygienePropertyDefinition("SenderAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000834 RID: 2100
		internal static readonly HygienePropertyDefinition MessageSubjectDefinition = new HygienePropertyDefinition("MessageSubject", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000835 RID: 2101
		internal static readonly HygienePropertyDefinition MessageSizeDefinition = new HygienePropertyDefinition("MessageSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000836 RID: 2102
		internal static readonly HygienePropertyDefinition EventTypeDefinition = new HygienePropertyDefinition("EventType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000837 RID: 2103
		internal static readonly HygienePropertyDefinition ActionDefinition = new HygienePropertyDefinition("Action", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000838 RID: 2104
		internal static readonly HygienePropertyDefinition InternalMessageIdDefinition = new HygienePropertyDefinition("InternalMessageId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
