using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003A6 RID: 934
	[Cmdlet("Get", "MessageTrace")]
	[OutputType(new Type[]
	{
		typeof(MessageTrace)
	})]
	public sealed class GetMessageTrace : MtrtTask<MessageTrace>
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x0008B028 File Offset: 0x00089228
		public GetMessageTrace() : base("Microsoft.Exchange.Hygiene.Data.MessageTrace.Reports.MessageTrace, Microsoft.Exchange.Hygiene.Data")
		{
			this.MessageId = new MultiValuedProperty<string>();
			this.SenderAddress = new MultiValuedProperty<string>();
			this.RecipientAddress = new MultiValuedProperty<string>();
			this.Status = new MultiValuedProperty<string>();
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x0008B061 File Offset: 0x00089261
		// (set) Token: 0x060020D1 RID: 8401 RVA: 0x0008B069 File Offset: 0x00089269
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		[QueryParameter("MessageIdListQueryDefinition", new string[]
		{

		})]
		public MultiValuedProperty<string> MessageId { get; set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0008B072 File Offset: 0x00089272
		// (set) Token: 0x060020D3 RID: 8403 RVA: 0x0008B07A File Offset: 0x0008927A
		[QueryParameter("SenderAddressListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		[CmdletValidator("ValidateEmailAddress", new object[]
		{
			CmdletValidator.EmailAddress.Sender,
			CmdletValidator.WildcardValidationOptions.Allow
		})]
		public MultiValuedProperty<string> SenderAddress { get; set; }

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0008B083 File Offset: 0x00089283
		// (set) Token: 0x060020D5 RID: 8405 RVA: 0x0008B08B File Offset: 0x0008928B
		[CmdletValidator("ValidateEmailAddress", new object[]
		{
			CmdletValidator.EmailAddress.Recipient,
			CmdletValidator.WildcardValidationOptions.Allow
		})]
		[QueryParameter("RecipientAddressListQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public MultiValuedProperty<string> RecipientAddress { get; set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x0008B094 File Offset: 0x00089294
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x0008B09C File Offset: 0x0008929C
		[Parameter(Mandatory = false)]
		[QueryParameter("MailDeliveryStatusListDefinition", new string[]
		{

		})]
		[CmdletValidator("ValidateEnum", new object[]
		{
			typeof(Schema.DeliveryStatusValues)
		}, ErrorMessage = Strings.IDs.InvalidDeliveryStatus)]
		public MultiValuedProperty<string> Status { get; set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x0008B0A5 File Offset: 0x000892A5
		// (set) Token: 0x060020D9 RID: 8409 RVA: 0x0008B0AD File Offset: 0x000892AD
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		[QueryParameter("InternalMessageIdQueryDefinition", new string[]
		{

		})]
		public Guid? MessageTraceId { get; set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x0008B0B6 File Offset: 0x000892B6
		// (set) Token: 0x060020DB RID: 8411 RVA: 0x0008B0BE File Offset: 0x000892BE
		[QueryParameter("ToIPAddressQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string ToIP { get; set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x0008B0C7 File Offset: 0x000892C7
		// (set) Token: 0x060020DD RID: 8413 RVA: 0x0008B0CF File Offset: 0x000892CF
		[QueryParameter("FromIPAddressQueryDefinition", new string[]
		{

		})]
		[Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
		public string FromIP { get; set; }

		// Token: 0x060020DE RID: 8414 RVA: 0x0008B0D8 File Offset: 0x000892D8
		protected override void CustomInternalValidate()
		{
			base.CustomInternalValidate();
			bool flag = false;
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			foreach (string text in this.MessageId)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string item = text;
					bool flag2 = text[0] != '<' && text[text.Length - 1] != '>';
					if (flag2)
					{
						item = '<' + text + '>';
						flag = true;
					}
					multiValuedProperty.Add(item);
				}
			}
			if (flag)
			{
				this.MessageId = multiValuedProperty;
			}
		}
	}
}
