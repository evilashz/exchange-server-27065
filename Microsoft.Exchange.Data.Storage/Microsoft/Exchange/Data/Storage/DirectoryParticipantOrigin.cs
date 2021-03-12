using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000928 RID: 2344
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DirectoryParticipantOrigin : ParticipantOrigin
	{
		// Token: 0x06005793 RID: 22419 RVA: 0x0016825D File Offset: 0x0016645D
		public DirectoryParticipantOrigin()
		{
		}

		// Token: 0x06005794 RID: 22420 RVA: 0x00168265 File Offset: 0x00166465
		public DirectoryParticipantOrigin(ADRawEntry adEntry)
		{
			this.adEntry = adEntry;
		}

		// Token: 0x06005795 RID: 22421 RVA: 0x00168274 File Offset: 0x00166474
		public DirectoryParticipantOrigin(IExchangePrincipal principal)
		{
			this.principal = principal;
		}

		// Token: 0x06005796 RID: 22422 RVA: 0x00168283 File Offset: 0x00166483
		public DirectoryParticipantOrigin(IStorePropertyBag adContact)
		{
			this.adContact = adContact;
		}

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06005797 RID: 22423 RVA: 0x00168292 File Offset: 0x00166492
		public ADRawEntry ADEntry
		{
			get
			{
				return this.adEntry;
			}
		}

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x0016829A File Offset: 0x0016649A
		public IExchangePrincipal Principal
		{
			get
			{
				return this.principal;
			}
		}

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06005799 RID: 22425 RVA: 0x001682A2 File Offset: 0x001664A2
		public IStorePropertyBag ADContact
		{
			get
			{
				return this.adContact;
			}
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x001682AA File Offset: 0x001664AA
		public override string ToString()
		{
			return "Directory";
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x001682B1 File Offset: 0x001664B1
		internal override IEnumerable<PropValue> GetProperties()
		{
			if (this.adEntry != null)
			{
				return DirectoryParticipantOrigin.GetProperties(this.adEntry);
			}
			if (this.principal != null)
			{
				return DirectoryParticipantOrigin.GetProperties(this.principal);
			}
			return null;
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x001682DC File Offset: 0x001664DC
		internal override ParticipantValidationStatus Validate(Participant participant)
		{
			if (participant.EmailAddress == null || participant.RoutingType == null)
			{
				return ParticipantValidationStatus.AddressAndOriginMismatch;
			}
			return ParticipantValidationStatus.NoError;
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x001682F4 File Offset: 0x001664F4
		private static IEnumerable<PropValue> GetProperties(ADRawEntry adEntry)
		{
			List<PropValue> list = new List<PropValue>();
			ConversionPropertyBag conversionPropertyBag = new ConversionPropertyBag(adEntry, StoreToDirectorySchemaConverter.Instance);
			foreach (StorePropertyDefinition storePropertyDefinition in DirectoryParticipantOrigin.propertiesToDonate)
			{
				object obj = conversionPropertyBag.TryGetProperty(storePropertyDefinition);
				if (!PropertyError.IsPropertyError(obj))
				{
					list.Add(new PropValue(storePropertyDefinition, obj));
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x00168358 File Offset: 0x00166558
		private static IEnumerable<PropValue> GetProperties(IExchangePrincipal principal)
		{
			List<PropValue> list = new List<PropValue>();
			RemoteUserMailboxPrincipal remoteUserMailboxPrincipal = principal as RemoteUserMailboxPrincipal;
			if (remoteUserMailboxPrincipal != null)
			{
				list.Add(new PropValue(ParticipantSchema.SmtpAddress, remoteUserMailboxPrincipal.PrimarySmtpAddress.ToString()));
			}
			else
			{
				list.Add(new PropValue(ParticipantSchema.SmtpAddress, principal.MailboxInfo.PrimarySmtpAddress.ToString()));
			}
			return list;
		}

		// Token: 0x04002EBA RID: 11962
		private readonly ADRawEntry adEntry;

		// Token: 0x04002EBB RID: 11963
		private readonly IExchangePrincipal principal;

		// Token: 0x04002EBC RID: 11964
		private readonly IStorePropertyBag adContact;

		// Token: 0x04002EBD RID: 11965
		private static readonly StorePropertyDefinition[] propertiesToDonate = new StorePropertyDefinition[]
		{
			ParticipantSchema.LegacyExchangeDN,
			ParticipantSchema.SmtpAddress,
			ParticipantSchema.DisplayTypeEx
		};
	}
}
