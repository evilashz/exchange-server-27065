using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000087 RID: 135
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OofReplyCreation : ReplyCreation
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x000459C8 File Offset: 0x00043BC8
		internal OofReplyCreation(MessageItem originalItem, MessageItem newItem, MessageItem template, ReplyForwardConfiguration configuration) : base(originalItem, newItem, configuration, false, true, true)
		{
			this.template = template;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000459E0 File Offset: 0x00043BE0
		protected override void BuildSubject()
		{
			this.newItem[InternalSchema.SubjectPrefix] = ClientStrings.OofReply.ToString(base.Culture);
			this.newItem[InternalSchema.NormalizedSubject] = this.originalItem.GetValueOrDefault<string>(InternalSchema.NormalizedSubjectInternal, string.Empty);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00045A38 File Offset: 0x00043C38
		protected override void UpdateNewItemProperties()
		{
			base.UpdateNewItemProperties();
			this.newItem[InternalSchema.OofReplyType] = this.template.GetValueOrDefault<int>(InternalSchema.OofReplyType, 2);
			this.newItem[InternalSchema.ItemClass] = this.template.GetValueOrDefault<string>(InternalSchema.ItemClass, "IPM.Note");
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00045A98 File Offset: 0x00043C98
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

		// Token: 0x0400027F RID: 639
		private readonly MessageItem template;

		// Token: 0x02000088 RID: 136
		internal enum OofReplyType
		{
			// Token: 0x04000281 RID: 641
			Legacy,
			// Token: 0x04000282 RID: 642
			Single,
			// Token: 0x04000283 RID: 643
			Internal,
			// Token: 0x04000284 RID: 644
			External
		}
	}
}
