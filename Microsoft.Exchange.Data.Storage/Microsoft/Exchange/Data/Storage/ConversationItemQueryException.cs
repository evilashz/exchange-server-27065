using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200071D RID: 1821
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ConversationItemQueryException : StoragePermanentException
	{
		// Token: 0x060047A4 RID: 18340 RVA: 0x001302F7 File Offset: 0x0012E4F7
		internal ConversationItemQueryException(LocalizedString message, PropertyDefinition unsupportedProperty) : base(message)
		{
			this.unsupportedProperty = unsupportedProperty;
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x00130307 File Offset: 0x0012E507
		protected ConversationItemQueryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.unsupportedProperty = (info.GetValue("unsupportedProperty", typeof(PropertyDefinition)) as PropertyDefinition);
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x00130331 File Offset: 0x0012E531
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("unsupportedProperty", this.unsupportedProperty);
		}

		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x060047A7 RID: 18343 RVA: 0x0013034C File Offset: 0x0012E54C
		public PropertyDefinition UnsupportedProperty
		{
			get
			{
				return this.unsupportedProperty;
			}
		}

		// Token: 0x04002724 RID: 10020
		private const string UnsupportedPropertyLabel = "unsupportedProperty";

		// Token: 0x04002725 RID: 10021
		private PropertyDefinition unsupportedProperty;
	}
}
