using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000772 RID: 1906
	[Serializable]
	public class ResolvePropertyDefinitionException : StoragePermanentException
	{
		// Token: 0x060048B1 RID: 18609 RVA: 0x0013165A File Offset: 0x0012F85A
		public ResolvePropertyDefinitionException(uint unresolvablePropertyTag, LocalizedString message) : base(message, null)
		{
			this.unresolvablePropertyTag = unresolvablePropertyTag;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x0013166B File Offset: 0x0012F86B
		protected ResolvePropertyDefinitionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.unresolvablePropertyTag = (uint)info.GetValue("unresolvablePropertyTag", typeof(uint));
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x00131695 File Offset: 0x0012F895
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("unresolvablePropertyTag", this.unresolvablePropertyTag);
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x060048B4 RID: 18612 RVA: 0x001316B0 File Offset: 0x0012F8B0
		public uint UnresolvablePropertyTag
		{
			get
			{
				return this.unresolvablePropertyTag;
			}
		}

		// Token: 0x0400276E RID: 10094
		private const string UnresolvablePropertyTagLabel = "unresolvablePropertyTag";

		// Token: 0x0400276F RID: 10095
		private readonly uint unresolvablePropertyTag;
	}
}
