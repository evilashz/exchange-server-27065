using System;
using System.Collections.Generic;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DC6 RID: 3526
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SharingContextSerializer
	{
		// Token: 0x06007947 RID: 31047 RVA: 0x00217666 File Offset: 0x00215866
		internal SharingContextSerializer(SharingContext context)
		{
			Util.ThrowOnNullArgument(context, "context");
			this.context = context;
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x00217680 File Offset: 0x00215880
		private static SharingMessageType CalculateSharingMessageType(SharingMessage sharingMessage)
		{
			if (sharingMessage.Invitation != null && sharingMessage.Request != null)
			{
				return SharingMessageType.InvitationAndRequest;
			}
			if (sharingMessage.Invitation != null)
			{
				return SharingMessageType.Invitation;
			}
			if (sharingMessage.Request != null)
			{
				return SharingMessageType.Request;
			}
			if (sharingMessage.AcceptOfRequest != null)
			{
				return SharingMessageType.AcceptOfRequest;
			}
			if (sharingMessage.DenyOfRequest != null)
			{
				return SharingMessageType.DenyOfRequest;
			}
			return SharingMessageType.Unknown;
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x002176E0 File Offset: 0x002158E0
		internal bool ReadFromMetadataXml(MessageItem messageItem)
		{
			SharingMessage sharingMessage = SharingMessageAttachment.GetSharingMessage(messageItem);
			if (sharingMessage == null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: The sharing_metadata.xml is invalid", messageItem.Session.UserLegacyDN);
				return false;
			}
			if (sharingMessage.Validate().Result != ValidationResult.Success)
			{
				ExTraceGlobals.SharingTracer.TraceError<string>((long)this.GetHashCode(), "{0}: The sharing_metadata.xml is invalid", messageItem.Session.UserLegacyDN);
				throw new InvalidSharingMessageException("SharingMetadata");
			}
			SharingDataType sharingDataType = SharingDataType.FromExternalName(sharingMessage.DataType);
			if (sharingDataType == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<string, string>((long)this.GetHashCode(), "{0}: Unknown sharing data type: {1}", messageItem.Session.UserLegacyDN, sharingMessage.DataType);
				throw new InvalidSharingMessageException("DataType");
			}
			this.context.FolderClass = sharingDataType.ContainerClass;
			if (!SmtpAddress.IsValidSmtpAddress(sharingMessage.Initiator.SmtpAddress))
			{
				ExTraceGlobals.SharingTracer.TraceError<string, string>((long)this.GetHashCode(), "{0}: Initiator's smtp address is invalid: {1}", messageItem.Session.UserLegacyDN, sharingMessage.Initiator.SmtpAddress);
				throw new InvalidSharingMessageException("InitiatorSmtpAddress");
			}
			this.context.InitiatorName = sharingMessage.Initiator.Name;
			this.context.InitiatorSmtpAddress = sharingMessage.Initiator.SmtpAddress;
			this.context.InitiatorEntryId = HexConverter.HexStringToByteArray(sharingMessage.Initiator.EntryId);
			this.context.AvailableSharingProviders.Clear();
			if (sharingMessage.Invitation != null)
			{
				this.ReadActionFromMetadataXml(sharingMessage.Invitation);
			}
			else if (sharingMessage.AcceptOfRequest != null)
			{
				this.ReadActionFromMetadataXml(sharingMessage.AcceptOfRequest);
			}
			else if (sharingMessage.Request != null)
			{
				this.ReadActionFromMetadataXml(sharingMessage.Request);
			}
			else if (sharingMessage.DenyOfRequest != null)
			{
				this.ReadActionFromMetadataXml(sharingMessage.DenyOfRequest);
			}
			if (this.context.AvailableSharingProviders.Count == 0)
			{
				ExTraceGlobals.SharingTracer.TraceError<string>((long)this.GetHashCode(), "{0}: No known sharing provider is found in message.", messageItem.Session.UserLegacyDN);
				throw new NotSupportedSharingMessageException();
			}
			if (this.context.IsPrimary)
			{
				this.context.FolderName = sharingDataType.DisplayName.ToString(messageItem.Session.InternalPreferedCulture);
			}
			this.context.SetDefaultCapabilities();
			SharingMessageType sharingMessageType = SharingContextSerializer.CalculateSharingMessageType(sharingMessage);
			if (sharingMessageType == SharingMessageType.Unknown)
			{
				ExTraceGlobals.SharingTracer.TraceError<string>((long)this.GetHashCode(), "{0}: SharingMessageType is unknown", messageItem.Session.UserLegacyDN);
				throw new InvalidSharingMessageException("SharingMessageType");
			}
			this.context.SharingMessageType = sharingMessageType;
			return true;
		}

		// Token: 0x0600794A RID: 31050 RVA: 0x00217958 File Offset: 0x00215B58
		private void ReadActionFromMetadataXml(SharingMessageAction action)
		{
			if (action != null)
			{
				this.context.IsPrimary = string.IsNullOrEmpty(action.Title);
				if (!this.context.IsPrimary)
				{
					this.context.FolderName = action.Title;
				}
				foreach (SharingMessageProvider sharingMessageProvider in action.Providers)
				{
					string[] recipients = sharingMessageProvider.TargetRecipients.Split(new char[]
					{
						';'
					});
					SharingProvider sharingProvider = SharingProvider.FromExternalName(sharingMessageProvider.Type);
					if (sharingProvider != null)
					{
						sharingProvider.ParseSharingMessageProvider(this.context, sharingMessageProvider);
						this.context.AvailableSharingProviders.Add(sharingProvider, new CheckRecipientsResults(ValidRecipient.ConvertFromStringArray(recipients)));
					}
				}
			}
		}

		// Token: 0x0600794B RID: 31051 RVA: 0x00217A14 File Offset: 0x00215C14
		internal void SaveIntoMetadataXml(MessageItem messageItem)
		{
			SharingMessage sharingMessage = this.CreateSharingMessage();
			SharingMessageAttachment.SetSharingMessage(messageItem, sharingMessage);
		}

		// Token: 0x0600794C RID: 31052 RVA: 0x00217A30 File Offset: 0x00215C30
		private SharingMessage CreateSharingMessage()
		{
			if (this.context.SharingMessageType == SharingMessageType.Invitation)
			{
				return this.CreateInvitationMessage();
			}
			if (this.context.SharingMessageType == SharingMessageType.Request)
			{
				return this.CreateRequestMessage();
			}
			if (this.context.SharingMessageType == SharingMessageType.InvitationAndRequest)
			{
				return this.CreateInvitationAndRequestMessage();
			}
			if (this.context.SharingMessageType == SharingMessageType.AcceptOfRequest)
			{
				return this.CreateAcceptOfRequestMessage();
			}
			if (this.context.SharingMessageType == SharingMessageType.DenyOfRequest)
			{
				return this.CreateDenyOfRequestMessage();
			}
			throw new NotSupportedException("SharingMessageType is not supported");
		}

		// Token: 0x0600794D RID: 31053 RVA: 0x00217AC4 File Offset: 0x00215CC4
		private SharingMessage CreateRequestMessage()
		{
			return new SharingMessage
			{
				DataType = this.context.DataType.ExternalName,
				Initiator = this.CreateInitiator(),
				Request = this.CreateRequestAction()
			};
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x00217B08 File Offset: 0x00215D08
		private SharingMessage CreateInvitationMessage()
		{
			return new SharingMessage
			{
				DataType = this.context.DataType.ExternalName,
				Initiator = this.CreateInitiator(),
				Invitation = this.CreateInvitationAction()
			};
		}

		// Token: 0x0600794F RID: 31055 RVA: 0x00217B4C File Offset: 0x00215D4C
		private SharingMessage CreateInvitationAndRequestMessage()
		{
			return new SharingMessage
			{
				DataType = this.context.DataType.ExternalName,
				Initiator = this.CreateInitiator(),
				Invitation = this.CreateInvitationAction(),
				Request = this.CreateRequestAction()
			};
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x00217B9C File Offset: 0x00215D9C
		private SharingMessage CreateAcceptOfRequestMessage()
		{
			return new SharingMessage
			{
				DataType = this.context.DataType.ExternalName,
				Initiator = this.CreateInitiator(),
				AcceptOfRequest = this.CreateInvitationAction()
			};
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x00217BE0 File Offset: 0x00215DE0
		private SharingMessage CreateDenyOfRequestMessage()
		{
			return new SharingMessage
			{
				DataType = this.context.DataType.ExternalName,
				Initiator = this.CreateInitiator(),
				DenyOfRequest = this.CreateRequestAction()
			};
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x00217C24 File Offset: 0x00215E24
		private SharingMessageAction CreateInvitationAction()
		{
			List<SharingMessageProvider> list = new List<SharingMessageProvider>(this.context.AvailableSharingProviders.Count);
			foreach (KeyValuePair<SharingProvider, CheckRecipientsResults> keyValuePair in this.context.AvailableSharingProviders)
			{
				SharingProvider key = keyValuePair.Key;
				CheckRecipientsResults value = keyValuePair.Value;
				if (value != null && value.ValidRecipients != null && value.ValidRecipients.Length > 0)
				{
					SharingMessageProvider sharingMessageProvider = key.CreateSharingMessageProvider(this.context);
					sharingMessageProvider.TargetRecipients = value.TargetRecipients;
					list.Add(sharingMessageProvider);
				}
			}
			return new SharingMessageAction
			{
				Title = (this.context.IsPrimary ? null : this.context.FolderName),
				Providers = list.ToArray()
			};
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x00217D10 File Offset: 0x00215F10
		private SharingMessageAction CreateRequestAction()
		{
			List<SharingMessageProvider> list = new List<SharingMessageProvider>(this.context.AvailableSharingProviders.Count);
			foreach (KeyValuePair<SharingProvider, CheckRecipientsResults> keyValuePair in this.context.AvailableSharingProviders)
			{
				SharingProvider key = keyValuePair.Key;
				CheckRecipientsResults value = keyValuePair.Value;
				if (key != SharingProvider.SharingProviderPublish && value != null && value.ValidRecipients != null && value.ValidRecipients.Length > 0)
				{
					SharingMessageProvider sharingMessageProvider = key.CreateSharingMessageProvider();
					sharingMessageProvider.TargetRecipients = value.TargetRecipients;
					list.Add(sharingMessageProvider);
				}
			}
			return new SharingMessageAction
			{
				Providers = list.ToArray()
			};
		}

		// Token: 0x06007954 RID: 31060 RVA: 0x00217DDC File Offset: 0x00215FDC
		private SharingMessageInitiator CreateInitiator()
		{
			return new SharingMessageInitiator
			{
				Name = this.context.InitiatorName,
				SmtpAddress = this.context.InitiatorSmtpAddress,
				EntryId = HexConverter.ByteArrayToHexString(this.context.InitiatorEntryId)
			};
		}

		// Token: 0x040053BF RID: 21439
		private SharingContext context;
	}
}
