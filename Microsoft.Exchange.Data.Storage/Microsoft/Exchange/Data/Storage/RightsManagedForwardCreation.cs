using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000862 RID: 2146
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RightsManagedForwardCreation : ForwardCreation
	{
		// Token: 0x060050D7 RID: 20695 RVA: 0x001505A4 File Offset: 0x0014E7A4
		internal RightsManagedForwardCreation(RightsManagedMessageItem originalItem, RightsManagedMessageItem message, ReplyForwardConfiguration parameters) : base(originalItem, message, parameters)
		{
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x001505B0 File Offset: 0x0014E7B0
		protected override void BuildBody(BodyConversionCallbacks callbacks)
		{
			RightsManagedMessageItem rightsManagedMessageItem = this.originalItem as RightsManagedMessageItem;
			RightsManagedMessageItem rightsManagedMessageItem2 = this.newItem as RightsManagedMessageItem;
			ReplyForwardCommon.CopyBodyWithPrefix(rightsManagedMessageItem.ProtectedBody, rightsManagedMessageItem2.ProtectedBody, this.parameters, callbacks);
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x001505F0 File Offset: 0x0014E7F0
		protected override void BuildAttachments(BodyConversionCallbacks callbacks, InboundConversionOptions optionsForSmime)
		{
			RightsManagedMessageItem rightsManagedMessageItem = this.originalItem as RightsManagedMessageItem;
			RightsManagedMessageItem rightsManagedMessageItem2 = this.newItem as RightsManagedMessageItem;
			base.CopyAttachments(callbacks, rightsManagedMessageItem.ProtectedAttachmentCollection, rightsManagedMessageItem2.ProtectedAttachmentCollection, false, this.parameters.TargetFormat == BodyFormat.TextPlain, optionsForSmime);
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x00150638 File Offset: 0x0014E838
		protected override BodyConversionCallbacks GetCallbacks()
		{
			RightsManagedMessageItem rightsManagedMessageItem = this.originalItem as RightsManagedMessageItem;
			return base.GetCallbacksInternal(rightsManagedMessageItem.ProtectedBody, rightsManagedMessageItem.ProtectedAttachmentCollection);
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x00150663 File Offset: 0x0014E863
		protected override void UpdateNewItemProperties()
		{
			base.UpdateNewItemProperties();
			RightsManagedReplyCreation.CopyDrmProperties(this.originalItem, this.newItem);
		}
	}
}
