using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DD4 RID: 3540
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharingMessageType
	{
		// Token: 0x060079B9 RID: 31161 RVA: 0x0021A620 File Offset: 0x00218820
		private SharingMessageType(string name, SharingFlavor sharingFlavor)
		{
			this.name = name;
			this.sharingFlavor = sharingFlavor;
		}

		// Token: 0x17002099 RID: 8345
		// (get) Token: 0x060079BA RID: 31162 RVA: 0x0021A636 File Offset: 0x00218836
		public bool IsRequest
		{
			get
			{
				return this == SharingMessageType.Request || this == SharingMessageType.InvitationAndRequest;
			}
		}

		// Token: 0x1700209A RID: 8346
		// (get) Token: 0x060079BB RID: 31163 RVA: 0x0021A64A File Offset: 0x0021884A
		public bool IsResponseToRequest
		{
			get
			{
				return this == SharingMessageType.AcceptOfRequest || this == SharingMessageType.DenyOfRequest;
			}
		}

		// Token: 0x1700209B RID: 8347
		// (get) Token: 0x060079BC RID: 31164 RVA: 0x0021A65E File Offset: 0x0021885E
		public bool IsInvitationOrRequest
		{
			get
			{
				return this == SharingMessageType.Invitation || this == SharingMessageType.Request || this == SharingMessageType.InvitationAndRequest;
			}
		}

		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x060079BD RID: 31165 RVA: 0x0021A67A File Offset: 0x0021887A
		public bool IsInvitationOrAcceptOfRequest
		{
			get
			{
				return this == SharingMessageType.Invitation || this == SharingMessageType.AcceptOfRequest || this == SharingMessageType.InvitationAndRequest;
			}
		}

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x060079BE RID: 31166 RVA: 0x0021A696 File Offset: 0x00218896
		internal SharingFlavor SharingFlavor
		{
			get
			{
				return this.sharingFlavor;
			}
		}

		// Token: 0x060079BF RID: 31167 RVA: 0x0021A6A0 File Offset: 0x002188A0
		public static SharingMessageType GetSharingMessageType(SharingFlavor sharingFlavor)
		{
			EnumValidator.ThrowIfInvalid<SharingFlavor>(sharingFlavor, "sharingFlavor");
			if ((sharingFlavor & SharingFlavor.SharingMessageInvitation) == SharingFlavor.SharingMessageInvitation && (sharingFlavor & SharingFlavor.SharingMessageRequest) == SharingFlavor.SharingMessageRequest)
			{
				return SharingMessageType.InvitationAndRequest;
			}
			if ((sharingFlavor & SharingFlavor.SharingMessageAccept) == SharingFlavor.SharingMessageAccept)
			{
				return SharingMessageType.AcceptOfRequest;
			}
			if ((sharingFlavor & SharingFlavor.SharingMessageInvitation) == SharingFlavor.SharingMessageInvitation)
			{
				return SharingMessageType.Invitation;
			}
			if ((sharingFlavor & SharingFlavor.SharingMessageRequest) == SharingFlavor.SharingMessageRequest)
			{
				return SharingMessageType.Request;
			}
			if ((sharingFlavor & SharingFlavor.SharingMessageDeny) == SharingFlavor.SharingMessageDeny)
			{
				return SharingMessageType.DenyOfRequest;
			}
			return SharingMessageType.Unknown;
		}

		// Token: 0x060079C0 RID: 31168 RVA: 0x0021A72F File Offset: 0x0021892F
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x04005402 RID: 21506
		public static readonly SharingMessageType Invitation = new SharingMessageType("Invitation", SharingFlavor.SharingOut | SharingFlavor.SharingMessage | SharingFlavor.SharingMessageInvitation);

		// Token: 0x04005403 RID: 21507
		public static readonly SharingMessageType Request = new SharingMessageType("Request", SharingFlavor.SharingMessage | SharingFlavor.SharingMessageRequest);

		// Token: 0x04005404 RID: 21508
		public static readonly SharingMessageType InvitationAndRequest = new SharingMessageType("InvitationAndRequest", SharingFlavor.SharingOut | SharingFlavor.SharingMessage | SharingFlavor.SharingMessageInvitation | SharingFlavor.SharingMessageRequest);

		// Token: 0x04005405 RID: 21509
		public static readonly SharingMessageType AcceptOfRequest = new SharingMessageType("AcceptOfRequest", SharingFlavor.SharingOut | SharingFlavor.SharingMessage | SharingFlavor.SharingMessageInvitation | SharingFlavor.SharingMessageResponse | SharingFlavor.SharingMessageAccept);

		// Token: 0x04005406 RID: 21510
		public static readonly SharingMessageType DenyOfRequest = new SharingMessageType("DenyOfRequest", SharingFlavor.SharingMessage | SharingFlavor.SharingMessageResponse | SharingFlavor.SharingMessageDeny);

		// Token: 0x04005407 RID: 21511
		public static readonly SharingMessageType Unknown = new SharingMessageType("Unknown", SharingFlavor.None);

		// Token: 0x04005408 RID: 21512
		private SharingFlavor sharingFlavor;

		// Token: 0x04005409 RID: 21513
		private string name;
	}
}
