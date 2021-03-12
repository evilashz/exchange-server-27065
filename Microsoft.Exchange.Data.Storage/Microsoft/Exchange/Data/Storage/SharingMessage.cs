using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DF5 RID: 3573
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sharing/2008", ElementName = "SharingMessage")]
	[Serializable]
	public sealed class SharingMessage
	{
		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x06007AC4 RID: 31428 RVA: 0x0021F10A File Offset: 0x0021D30A
		// (set) Token: 0x06007AC5 RID: 31429 RVA: 0x0021F112 File Offset: 0x0021D312
		[XmlElement]
		public string DataType { get; set; }

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x06007AC6 RID: 31430 RVA: 0x0021F11B File Offset: 0x0021D31B
		// (set) Token: 0x06007AC7 RID: 31431 RVA: 0x0021F123 File Offset: 0x0021D323
		[XmlElement]
		public SharingMessageInitiator Initiator { get; set; }

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x06007AC8 RID: 31432 RVA: 0x0021F12C File Offset: 0x0021D32C
		// (set) Token: 0x06007AC9 RID: 31433 RVA: 0x0021F134 File Offset: 0x0021D334
		[XmlElement(IsNullable = false)]
		public SharingMessageAction Invitation { get; set; }

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x06007ACA RID: 31434 RVA: 0x0021F13D File Offset: 0x0021D33D
		// (set) Token: 0x06007ACB RID: 31435 RVA: 0x0021F145 File Offset: 0x0021D345
		[XmlElement(IsNullable = false)]
		public SharingMessageAction Request { get; set; }

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x06007ACC RID: 31436 RVA: 0x0021F14E File Offset: 0x0021D34E
		// (set) Token: 0x06007ACD RID: 31437 RVA: 0x0021F156 File Offset: 0x0021D356
		[XmlElement(IsNullable = false)]
		public SharingMessageAction AcceptOfRequest { get; set; }

		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x06007ACE RID: 31438 RVA: 0x0021F15F File Offset: 0x0021D35F
		// (set) Token: 0x06007ACF RID: 31439 RVA: 0x0021F167 File Offset: 0x0021D367
		[XmlElement(IsNullable = false)]
		public SharingMessageAction DenyOfRequest { get; set; }

		// Token: 0x06007AD0 RID: 31440 RVA: 0x0021F170 File Offset: 0x0021D370
		internal static SharingMessage DeserializeFromStream(Stream stream)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SharingMessage));
			XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(stream);
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
			xmlTextReader.Normalization = true;
			return xmlSerializer.Deserialize(xmlTextReader) as SharingMessage;
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x0021F2C8 File Offset: 0x0021D4C8
		internal ValidationResults Validate()
		{
			if (string.IsNullOrEmpty(this.DataType))
			{
				return new ValidationResults(ValidationResult.Failure, "DataType is required");
			}
			if (this.Initiator == null)
			{
				return new ValidationResults(ValidationResult.Failure, "Initiator is required");
			}
			if (this.Invitation == null && this.Request == null && this.AcceptOfRequest == null && this.DenyOfRequest == null)
			{
				return new ValidationResults(ValidationResult.Failure, "At least one of the following are required: Invitation, Request, AcceptOfRequest or DenyOfRequest");
			}
			if (this.AcceptOfRequest != null && this.DenyOfRequest != null)
			{
				return new ValidationResults(ValidationResult.Failure, "AcceptOfRequest and DenyOfRequest are mutually exclusive");
			}
			ValidationResults validationResults = this.Initiator.Validate();
			if (validationResults.Result != ValidationResult.Success)
			{
				return validationResults;
			}
			var array = new <>f__AnonymousType2<SharingMessageAction, SharingMessageKind>[]
			{
				new
				{
					Action = this.Invitation,
					Kind = SharingMessageKind.Invitation
				},
				new
				{
					Action = this.Request,
					Kind = SharingMessageKind.Request
				},
				new
				{
					Action = this.AcceptOfRequest,
					Kind = SharingMessageKind.AcceptOfRequest
				},
				new
				{
					Action = this.DenyOfRequest,
					Kind = SharingMessageKind.DenyOfRequest
				}
			};
			var array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				var <>f__AnonymousType = array2[i];
				if (<>f__AnonymousType.Action != null)
				{
					validationResults = <>f__AnonymousType.Action.Validate(<>f__AnonymousType.Kind);
					if (validationResults.Result != ValidationResult.Success)
					{
						return validationResults;
					}
				}
			}
			return ValidationResults.Success;
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x0021F3F4 File Offset: 0x0021D5F4
		internal void SerializeToStream(Stream stream)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SharingMessage));
			xmlSerializer.Serialize(stream, this);
		}
	}
}
