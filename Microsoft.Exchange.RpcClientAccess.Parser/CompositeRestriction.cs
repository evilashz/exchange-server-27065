using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000205 RID: 517
	internal abstract class CompositeRestriction : Restriction
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x0002418B File Offset: 0x0002238B
		internal CompositeRestriction(Restriction[] childRestrictions)
		{
			if (childRestrictions == null)
			{
				throw new ArgumentNullException("childRestrictions");
			}
			this.childRestrictions = childRestrictions;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000241A8 File Offset: 0x000223A8
		protected static T InternalParse<T>(Reader reader, CompositeRestriction.Creator<T> creator, WireFormatStyle wireFormatStyle, uint depth) where T : CompositeRestriction
		{
			uint num = reader.ReadCountOrSize(wireFormatStyle);
			reader.CheckBoundary(num, 1U);
			Restriction[] array = new Restriction[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				array[num2] = Restriction.InternalParse(reader, wireFormatStyle, depth);
				num2++;
			}
			return creator(array);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000241F0 File Offset: 0x000223F0
		public override void Serialize(Writer writer, Encoding string8Encoding, WireFormatStyle wireFormatStyle)
		{
			base.Serialize(writer, string8Encoding, wireFormatStyle);
			writer.WriteCountOrSize(this.childRestrictions.Length, wireFormatStyle);
			foreach (Restriction restriction in this.childRestrictions)
			{
				restriction.Serialize(writer, string8Encoding, wireFormatStyle);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00024237 File Offset: 0x00022437
		public Restriction[] ChildRestrictions
		{
			get
			{
				return this.childRestrictions;
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00024240 File Offset: 0x00022440
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			foreach (Restriction restriction in this.childRestrictions)
			{
				restriction.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00024274 File Offset: 0x00022474
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" [");
			if (this.childRestrictions != null)
			{
				for (int i = 0; i < this.childRestrictions.Length; i++)
				{
					stringBuilder.Append("[");
					if (this.childRestrictions[i] != null)
					{
						this.childRestrictions[i].AppendToString(stringBuilder);
					}
					stringBuilder.Append("]");
					if (i < this.childRestrictions.Length - 1)
					{
						stringBuilder.Append(", ");
					}
				}
			}
			stringBuilder.Append("]");
		}

		// Token: 0x0400066D RID: 1645
		private readonly Restriction[] childRestrictions;

		// Token: 0x02000206 RID: 518
		// (Invoke) Token: 0x06000B49 RID: 2889
		protected delegate T Creator<T>(Restriction[] childRestriction) where T : CompositeRestriction;
	}
}
