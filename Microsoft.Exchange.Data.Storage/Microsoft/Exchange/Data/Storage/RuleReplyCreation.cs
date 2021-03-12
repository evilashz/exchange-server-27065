using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000086 RID: 134
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RuleReplyCreation : ReplyCreation
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x000457E2 File Offset: 0x000439E2
		internal RuleReplyCreation(MessageItem originalItem, MessageItem newItem, MessageItem template, ReplyForwardConfiguration configuration) : this(originalItem, newItem, template, configuration, false)
		{
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x000457F0 File Offset: 0x000439F0
		internal RuleReplyCreation(MessageItem originalItem, MessageItem newItem, MessageItem template, ReplyForwardConfiguration configuration, bool shouldUseSender) : base(originalItem, newItem, configuration, false, shouldUseSender, false)
		{
			this.template = template;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00045808 File Offset: 0x00043A08
		protected override void UpdateNewItemProperties()
		{
			base.UpdateNewItemProperties();
			MessageItem messageItem = (MessageItem)this.newItem;
			if (this.template.ReplyTo.Count != 0 && messageItem.ReplyTo.Count == 0)
			{
				messageItem.PropertyBag[InternalSchema.MapiReplyToNames] = this.template.PropertyBag.GetValueOrDefault<string>(InternalSchema.MapiReplyToNames);
				messageItem.PropertyBag[InternalSchema.MapiReplyToBlob] = this.template.PropertyBag.GetValueOrDefault<byte[]>(InternalSchema.MapiReplyToBlob);
			}
			this.newItem.ClassName = this.template.ClassName;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x000458A8 File Offset: 0x00043AA8
		protected override void BuildBody(BodyConversionCallbacks callbacks)
		{
			BodyReadConfiguration configuration = new BodyReadConfiguration(this.template.Body.RawFormat, this.template.Body.RawCharset.Name);
			BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(this.template.Body.RawFormat, this.template.Body.RawCharset.Name);
			bodyWriteConfiguration.SetTargetFormat(this.template.Body.Format, this.template.Body.Charset);
			using (Stream stream = this.template.Body.OpenReadStream(configuration))
			{
				using (Stream stream2 = this.newItem.Body.OpenWriteStream(bodyWriteConfiguration))
				{
					Util.StreamHandler.CopyStreamData(stream, stream2);
				}
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00045994 File Offset: 0x00043B94
		protected override void BuildAttachments(BodyConversionCallbacks callbacks, InboundConversionOptions optionsForSmime)
		{
			base.CopyAttachments(callbacks, this.template.AttachmentCollection, this.newItem.AttachmentCollection, false, this.template.Body.Format == BodyFormat.TextPlain, optionsForSmime);
		}

		// Token: 0x0400027E RID: 638
		private readonly MessageItem template;
	}
}
