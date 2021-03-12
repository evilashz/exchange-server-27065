using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000212 RID: 530
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class Restriction : IEquatable<Restriction>
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x0002E7D8 File Offset: 0x0002C9D8
		public static Restriction And(params Restriction[] restrictions)
		{
			return new Restriction.AndRestriction(restrictions);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002E7E0 File Offset: 0x0002C9E0
		public static Restriction Near(int distance, bool ordered, Restriction.AndRestriction restriction)
		{
			return new Restriction.NearRestriction(distance, ordered, restriction);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002E7EA File Offset: 0x0002C9EA
		public static Restriction BitMask(Restriction.RelBmr relBmr, PropTag tag, int mask)
		{
			return new Restriction.BitMaskRestriction(relBmr, tag, mask);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		public static Restriction BitMaskZero(PropTag tag, int mask)
		{
			return new Restriction.BitMaskRestriction(Restriction.RelBmr.EqualToZero, tag, mask);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002E7FE File Offset: 0x0002C9FE
		public static Restriction BitMaskNonZero(PropTag tag, int mask)
		{
			return new Restriction.BitMaskRestriction(Restriction.RelBmr.NotEqualToZero, tag, mask);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002E808 File Offset: 0x0002CA08
		public static Restriction Comment(Restriction restriction, params PropValue[] propValues)
		{
			return new Restriction.CommentRestriction(restriction, propValues);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002E811 File Offset: 0x0002CA11
		public static Restriction MemberOf(PropTag tag, object value)
		{
			return new Restriction.PropertyRestriction(Restriction.RelOp.MemberOfDL, tag, value);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002E81C File Offset: 0x0002CA1C
		public static Restriction Property(Restriction.RelOp relOp, PropTag tag, bool multiValued, PropValue value)
		{
			return new Restriction.PropertyRestriction(relOp, tag, multiValued, value);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002E827 File Offset: 0x0002CA27
		public static Restriction GE(PropTag tag, object value)
		{
			return new Restriction.PropertyRestriction(Restriction.RelOp.GreaterThanOrEqual, tag, value);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002E831 File Offset: 0x0002CA31
		public static Restriction GT(PropTag tag, object value)
		{
			return new Restriction.PropertyRestriction(Restriction.RelOp.GreaterThan, tag, value);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002E83B File Offset: 0x0002CA3B
		public static Restriction LE(PropTag tag, object value)
		{
			return new Restriction.PropertyRestriction(Restriction.RelOp.LessThanOrEqual, tag, value);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002E845 File Offset: 0x0002CA45
		public static Restriction LT(PropTag tag, object value)
		{
			return new Restriction.PropertyRestriction(Restriction.RelOp.LessThan, tag, value);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002E84F File Offset: 0x0002CA4F
		public static Restriction NE(PropTag tag, object value)
		{
			return new Restriction.PropertyRestriction(Restriction.RelOp.NotEqual, tag, value);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002E859 File Offset: 0x0002CA59
		public static Restriction RE(PropTag tag, string value)
		{
			return new Restriction.PropertyRestriction(Restriction.RelOp.RegularExpression, tag, value);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002E863 File Offset: 0x0002CA63
		public static Restriction EQ(PropTag tag, object value)
		{
			return new Restriction.PropertyRestriction(Restriction.RelOp.Equal, tag, value);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002E86D File Offset: 0x0002CA6D
		public static Restriction CompareProps(Restriction.RelOp relOp, PropTag tag1, PropTag tag2)
		{
			return new Restriction.ComparePropertyRestriction(relOp, tag1, tag2);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002E877 File Offset: 0x0002CA77
		public static Restriction GE(PropTag tag1, PropTag tag2)
		{
			return new Restriction.ComparePropertyRestriction(Restriction.RelOp.GreaterThanOrEqual, tag1, tag2);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002E881 File Offset: 0x0002CA81
		public static Restriction GT(PropTag tag1, PropTag tag2)
		{
			return new Restriction.ComparePropertyRestriction(Restriction.RelOp.GreaterThan, tag1, tag2);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002E88B File Offset: 0x0002CA8B
		public static Restriction LE(PropTag tag1, PropTag tag2)
		{
			return new Restriction.ComparePropertyRestriction(Restriction.RelOp.LessThanOrEqual, tag1, tag2);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002E895 File Offset: 0x0002CA95
		public static Restriction LT(PropTag tag1, PropTag tag2)
		{
			return new Restriction.ComparePropertyRestriction(Restriction.RelOp.LessThan, tag1, tag2);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002E89F File Offset: 0x0002CA9F
		public static Restriction NE(PropTag tag1, PropTag tag2)
		{
			return new Restriction.ComparePropertyRestriction(Restriction.RelOp.NotEqual, tag1, tag2);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002E8A9 File Offset: 0x0002CAA9
		public static Restriction RE(PropTag tag1, PropTag tag2)
		{
			return new Restriction.ComparePropertyRestriction(Restriction.RelOp.RegularExpression, tag1, tag2);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002E8B3 File Offset: 0x0002CAB3
		public static Restriction EQ(PropTag tag1, PropTag tag2)
		{
			return new Restriction.ComparePropertyRestriction(Restriction.RelOp.Equal, tag1, tag2);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002E8BD File Offset: 0x0002CABD
		public static Restriction Content(PropTag tag, object value, ContentFlags flags)
		{
			return new Restriction.ContentRestriction(tag, value, flags);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002E8C7 File Offset: 0x0002CAC7
		public static Restriction Content(PropTag tag, bool multiValued, PropValue value, ContentFlags flags)
		{
			return new Restriction.ContentRestriction(tag, multiValued, value, flags);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002E8D2 File Offset: 0x0002CAD2
		public static Restriction Exist(PropTag tag)
		{
			return new Restriction.ExistRestriction(tag);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002E8DA File Offset: 0x0002CADA
		public static Restriction Not(Restriction restriction)
		{
			return new Restriction.NotRestriction(restriction);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002E8E2 File Offset: 0x0002CAE2
		public static Restriction Count(int count, Restriction restriction)
		{
			return new Restriction.CountRestriction(count, restriction);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0002E8EB File Offset: 0x0002CAEB
		public static Restriction Or(params Restriction[] restrictions)
		{
			return new Restriction.OrRestriction(restrictions);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002E8F3 File Offset: 0x0002CAF3
		public static Restriction PropertySize(Restriction.RelOp relOp, PropTag tag, int cb)
		{
			return new Restriction.SizeRestriction(relOp, tag, cb);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002E8FD File Offset: 0x0002CAFD
		public static Restriction SizeGE(PropTag tag, int cb)
		{
			return new Restriction.SizeRestriction(Restriction.RelOp.GreaterThanOrEqual, tag, cb);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002E907 File Offset: 0x0002CB07
		public static Restriction SizeGT(PropTag tag, int cb)
		{
			return new Restriction.SizeRestriction(Restriction.RelOp.GreaterThan, tag, cb);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002E911 File Offset: 0x0002CB11
		public static Restriction SizeLE(PropTag tag, int cb)
		{
			return new Restriction.SizeRestriction(Restriction.RelOp.LessThanOrEqual, tag, cb);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002E91B File Offset: 0x0002CB1B
		public static Restriction SizeLT(PropTag tag, int cb)
		{
			return new Restriction.SizeRestriction(Restriction.RelOp.LessThan, tag, cb);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0002E925 File Offset: 0x0002CB25
		public static Restriction SizeNE(PropTag tag, int cb)
		{
			return new Restriction.SizeRestriction(Restriction.RelOp.NotEqual, tag, cb);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002E92F File Offset: 0x0002CB2F
		public static Restriction SizeEQ(PropTag tag, int cb)
		{
			return new Restriction.SizeRestriction(Restriction.RelOp.Equal, tag, cb);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002E93C File Offset: 0x0002CB3C
		public static Restriction Sub(PropTag tag, Restriction restriction)
		{
			if (tag == PropTag.MessageRecipients)
			{
				return new Restriction.RecipientRestriction(restriction);
			}
			if (tag != PropTag.MessageAttachments)
			{
				return null;
			}
			return new Restriction.AttachmentRestriction(restriction);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0002E96C File Offset: 0x0002CB6C
		public static Restriction False()
		{
			return new Restriction.FalseRestriction();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002E973 File Offset: 0x0002CB73
		public static Restriction True()
		{
			return new Restriction.TrueRestriction();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002E97A File Offset: 0x0002CB7A
		public override bool Equals(object comparand)
		{
			return comparand is Restriction && this.Equals((Restriction)comparand);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002E992 File Offset: 0x0002CB92
		public bool Equals(Restriction comparand)
		{
			return this.IsEqualTo(comparand);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002E99B File Offset: 0x0002CB9B
		public static bool Equals(Restriction v1, Restriction v2)
		{
			return v1.Equals(v2);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002E9C4 File Offset: 0x0002CBC4
		public override int GetHashCode()
		{
			int result = 0;
			this.EnumerateRestriction(delegate(Restriction restriction, object ctx)
			{
				result = (int)(result + restriction.Type);
			}, null);
			return result;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0002E9F7 File Offset: 0x0002CBF7
		public Restriction.ResType Type
		{
			get
			{
				return this.resType;
			}
		}

		// Token: 0x060008F5 RID: 2293
		public abstract int GetBytesToMarshal();

		// Token: 0x060008F6 RID: 2294
		public abstract int GetBytesToMarshalNspi();

		// Token: 0x060008F7 RID: 2295
		internal unsafe abstract void MarshalToNative(SRestriction* psr, ref byte* pExtra);

		// Token: 0x060008F8 RID: 2296
		internal unsafe abstract void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra);

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002EA00 File Offset: 0x0002CC00
		public unsafe void MarshalToNative(SafeHandle handle)
		{
			SRestriction* ptr = (SRestriction*)handle.DangerousGetHandle().ToPointer();
			byte* ptr2 = (byte*)(ptr + (SRestriction.SizeOf + 7 & -8) / sizeof(SRestriction));
			this.MarshalToNative(ptr, ref ptr2);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002EA34 File Offset: 0x0002CC34
		public unsafe void MarshalToNativeNspi(SafeHandle handle)
		{
			SNspiRestriction* ptr = (SNspiRestriction*)handle.DangerousGetHandle().ToPointer();
			byte* ptr2 = (byte*)(ptr + (SNspiRestriction.SizeOf + 7 & -8) / sizeof(SNspiRestriction));
			this.MarshalToNative(ptr, ref ptr2);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002EA66 File Offset: 0x0002CC66
		internal virtual bool IsEqualTo(Restriction other)
		{
			return this.Type == other.Type;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002EA76 File Offset: 0x0002CC76
		private Restriction(Restriction.ResType resType)
		{
			this.resType = resType;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002EA85 File Offset: 0x0002CC85
		internal static Restriction Unmarshal(SafeHandle restriction)
		{
			return Restriction.Unmarshal(restriction.DangerousGetHandle());
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002EA92 File Offset: 0x0002CC92
		public unsafe static Restriction Unmarshal(IntPtr restriction)
		{
			return Restriction.Unmarshal((SRestriction*)restriction.ToPointer());
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002EAA0 File Offset: 0x0002CCA0
		internal unsafe static Restriction Unmarshal(SRestriction* psres)
		{
			if (null != psres)
			{
				Restriction.ResType rt = (Restriction.ResType)psres->rt;
				switch (rt)
				{
				case Restriction.ResType.And:
					return new Restriction.AndRestriction(psres);
				case Restriction.ResType.Or:
					return new Restriction.OrRestriction(psres);
				case Restriction.ResType.Not:
					return new Restriction.NotRestriction(psres);
				case Restriction.ResType.Content:
					return new Restriction.ContentRestriction(psres);
				case Restriction.ResType.Property:
					return new Restriction.PropertyRestriction(psres);
				case Restriction.ResType.CompareProps:
					return new Restriction.ComparePropertyRestriction(psres);
				case Restriction.ResType.BitMask:
					return new Restriction.BitMaskRestriction(psres);
				case Restriction.ResType.Size:
					return new Restriction.SizeRestriction(psres);
				case Restriction.ResType.Exist:
					return new Restriction.ExistRestriction(psres);
				case Restriction.ResType.SubRestriction:
				{
					PropTag ulSubObject = (PropTag)psres->union.resSub.ulSubObject;
					if (ulSubObject == PropTag.MessageRecipients)
					{
						return new Restriction.RecipientRestriction(psres);
					}
					if (ulSubObject != PropTag.MessageAttachments)
					{
						return null;
					}
					return new Restriction.AttachmentRestriction(psres);
				}
				case Restriction.ResType.Comment:
					return new Restriction.CommentRestriction(psres);
				case Restriction.ResType.Count:
					return new Restriction.CountRestriction(psres);
				case (Restriction.ResType)12:
					break;
				case Restriction.ResType.Near:
					return new Restriction.NearRestriction(psres);
				default:
					switch (rt)
					{
					case Restriction.ResType.True:
						return new Restriction.TrueRestriction(psres);
					case Restriction.ResType.False:
						return new Restriction.FalseRestriction(psres);
					}
					break;
				}
			}
			return null;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002EBA9 File Offset: 0x0002CDA9
		public unsafe static Restriction UnmarshalNspi(IntPtr restriction)
		{
			return Restriction.UnmarshalNspi((SNspiRestriction*)restriction.ToPointer());
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002EBB8 File Offset: 0x0002CDB8
		internal unsafe static Restriction UnmarshalNspi(SNspiRestriction* psres)
		{
			if (null != psres)
			{
				switch (psres->rt)
				{
				case 0:
					return new Restriction.AndRestriction(psres);
				case 1:
					return new Restriction.OrRestriction(psres);
				case 2:
					return new Restriction.NotRestriction(psres);
				case 3:
					return new Restriction.ContentRestriction(psres);
				case 4:
					return new Restriction.PropertyRestriction(psres);
				case 8:
					return new Restriction.ExistRestriction(psres);
				}
			}
			return null;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0002EC28 File Offset: 0x0002CE28
		public void EnumerateRestriction(Restriction.EnumRestrictionDelegate del, object ctx)
		{
			del(this, ctx);
			this.EnumerateSubRestrictions(del, ctx);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002EC3A File Offset: 0x0002CE3A
		internal virtual void EnumerateSubRestrictions(Restriction.EnumRestrictionDelegate del, object ctx)
		{
		}

		// Token: 0x04000F7D RID: 3965
		private Restriction.ResType resType;

		// Token: 0x02000213 RID: 531
		internal enum ResType
		{
			// Token: 0x04000F7F RID: 3967
			And,
			// Token: 0x04000F80 RID: 3968
			Or,
			// Token: 0x04000F81 RID: 3969
			Not,
			// Token: 0x04000F82 RID: 3970
			Content,
			// Token: 0x04000F83 RID: 3971
			Property,
			// Token: 0x04000F84 RID: 3972
			CompareProps,
			// Token: 0x04000F85 RID: 3973
			BitMask,
			// Token: 0x04000F86 RID: 3974
			Size,
			// Token: 0x04000F87 RID: 3975
			Exist,
			// Token: 0x04000F88 RID: 3976
			SubRestriction,
			// Token: 0x04000F89 RID: 3977
			Comment,
			// Token: 0x04000F8A RID: 3978
			Count,
			// Token: 0x04000F8B RID: 3979
			Near = 13,
			// Token: 0x04000F8C RID: 3980
			True = 131,
			// Token: 0x04000F8D RID: 3981
			False
		}

		// Token: 0x02000214 RID: 532
		internal enum RelOp
		{
			// Token: 0x04000F8F RID: 3983
			LessThan,
			// Token: 0x04000F90 RID: 3984
			LessThanOrEqual,
			// Token: 0x04000F91 RID: 3985
			GreaterThan,
			// Token: 0x04000F92 RID: 3986
			GreaterThanOrEqual,
			// Token: 0x04000F93 RID: 3987
			Equal,
			// Token: 0x04000F94 RID: 3988
			NotEqual,
			// Token: 0x04000F95 RID: 3989
			RegularExpression,
			// Token: 0x04000F96 RID: 3990
			Include = 16,
			// Token: 0x04000F97 RID: 3991
			Exclude,
			// Token: 0x04000F98 RID: 3992
			MemberOfDL = 100
		}

		// Token: 0x02000215 RID: 533
		internal enum RelBmr
		{
			// Token: 0x04000F9A RID: 3994
			EqualToZero,
			// Token: 0x04000F9B RID: 3995
			NotEqualToZero
		}

		// Token: 0x02000216 RID: 534
		// (Invoke) Token: 0x06000905 RID: 2309
		public delegate void EnumRestrictionDelegate(Restriction restriction, object ctx);

		// Token: 0x02000217 RID: 535
		public class AndOrNotRestriction : Restriction
		{
			// Token: 0x06000908 RID: 2312 RVA: 0x0002EC3C File Offset: 0x0002CE3C
			internal AndOrNotRestriction(Restriction.ResType resType, params Restriction[] restrictions) : base(resType)
			{
				this.restrictions = restrictions;
			}

			// Token: 0x06000909 RID: 2313 RVA: 0x0002EC4C File Offset: 0x0002CE4C
			internal unsafe AndOrNotRestriction(SRestriction* psres) : base((Restriction.ResType)psres->rt)
			{
				int num = (this.resType == Restriction.ResType.Not) ? 1 : psres->union.resAnd.cRes;
				this.restrictions = new Restriction[num];
				for (int i = 0; i < num; i++)
				{
					this.restrictions[i] = Restriction.Unmarshal(psres->union.resAnd.lpRes + i);
				}
			}

			// Token: 0x0600090A RID: 2314 RVA: 0x0002ECC4 File Offset: 0x0002CEC4
			internal unsafe AndOrNotRestriction(SNspiRestriction* psres) : base((Restriction.ResType)psres->rt)
			{
				int num = (this.resType == Restriction.ResType.Not) ? 1 : psres->union.resAnd.cRes;
				this.restrictions = new Restriction[num];
				for (int i = 0; i < num; i++)
				{
					this.restrictions[i] = Restriction.UnmarshalNspi(psres->union.resAnd.lpRes + i);
				}
			}

			// Token: 0x0600090B RID: 2315 RVA: 0x0002ED3C File Offset: 0x0002CF3C
			public override int GetBytesToMarshal()
			{
				int num = SRestriction.SizeOf + 7 & -8;
				foreach (Restriction restriction in this.restrictions)
				{
					num += (restriction.GetBytesToMarshal() + 7 & -8);
				}
				return num;
			}

			// Token: 0x0600090C RID: 2316 RVA: 0x0002ED7C File Offset: 0x0002CF7C
			public override int GetBytesToMarshalNspi()
			{
				int num = SNspiRestriction.SizeOf + 7 & -8;
				foreach (Restriction restriction in this.restrictions)
				{
					num += (restriction.GetBytesToMarshalNspi() + 7 & -8);
				}
				return num;
			}

			// Token: 0x0600090D RID: 2317 RVA: 0x0002EDBC File Offset: 0x0002CFBC
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				SRestriction* lpRes = pExtra;
				if (this.restrictions.Length > 0)
				{
					pExtra += (IntPtr)(SRestriction.SizeOf * this.restrictions.Length + 7 & -8);
				}
				psr->rt = (int)this.resType;
				psr->union.resAnd.cRes = this.restrictions.Length;
				if (this.restrictions.Length > 0)
				{
					psr->union.resAnd.lpRes = lpRes;
				}
				else
				{
					psr->union.resAnd.lpRes = null;
				}
				foreach (Restriction restriction in this.restrictions)
				{
					restriction.MarshalToNative(lpRes++, ref pExtra);
				}
			}

			// Token: 0x0600090E RID: 2318 RVA: 0x0002EE70 File Offset: 0x0002D070
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				SNspiRestriction* lpRes = pExtra;
				if (this.restrictions.Length > 0)
				{
					pExtra += (IntPtr)(SNspiRestriction.SizeOf * this.restrictions.Length + 7 & -8);
				}
				psr->rt = (int)this.resType;
				psr->union.resAnd.cRes = this.restrictions.Length;
				if (this.restrictions.Length > 0)
				{
					psr->union.resAnd.lpRes = lpRes;
				}
				else
				{
					psr->union.resAnd.lpRes = null;
				}
				foreach (Restriction restriction in this.restrictions)
				{
					restriction.MarshalToNative(lpRes++, ref pExtra);
				}
			}

			// Token: 0x0600090F RID: 2319 RVA: 0x0002EF24 File Offset: 0x0002D124
			internal override void EnumerateSubRestrictions(Restriction.EnumRestrictionDelegate del, object ctx)
			{
				foreach (Restriction restriction in this.restrictions)
				{
					restriction.EnumerateRestriction(del, ctx);
				}
			}

			// Token: 0x06000910 RID: 2320 RVA: 0x0002EF54 File Offset: 0x0002D154
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction[] array = ((Restriction.AndOrNotRestriction)other).restrictions;
				if (this.restrictions.Length != array.Length)
				{
					return false;
				}
				for (int i = 0; i < this.restrictions.Length; i++)
				{
					if (!this.restrictions[i].IsEqualTo(array[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04000F9C RID: 3996
			internal Restriction[] restrictions;
		}

		// Token: 0x02000218 RID: 536
		public class AndRestriction : Restriction.AndOrNotRestriction
		{
			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000911 RID: 2321 RVA: 0x0002EFAF File Offset: 0x0002D1AF
			// (set) Token: 0x06000912 RID: 2322 RVA: 0x0002EFB7 File Offset: 0x0002D1B7
			public Restriction[] Restrictions
			{
				get
				{
					return this.restrictions;
				}
				set
				{
					this.restrictions = value;
				}
			}

			// Token: 0x06000913 RID: 2323 RVA: 0x0002EFC0 File Offset: 0x0002D1C0
			public AndRestriction(params Restriction[] restrictions) : base(Restriction.ResType.And, restrictions)
			{
			}

			// Token: 0x06000914 RID: 2324 RVA: 0x0002EFCA File Offset: 0x0002D1CA
			internal unsafe AndRestriction(SRestriction* psres) : base(psres)
			{
			}

			// Token: 0x06000915 RID: 2325 RVA: 0x0002EFD3 File Offset: 0x0002D1D3
			internal unsafe AndRestriction(SNspiRestriction* psres) : base(psres)
			{
			}
		}

		// Token: 0x02000219 RID: 537
		public class OrRestriction : Restriction.AndOrNotRestriction
		{
			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000916 RID: 2326 RVA: 0x0002EFDC File Offset: 0x0002D1DC
			// (set) Token: 0x06000917 RID: 2327 RVA: 0x0002EFE4 File Offset: 0x0002D1E4
			public Restriction[] Restrictions
			{
				get
				{
					return this.restrictions;
				}
				set
				{
					this.restrictions = value;
				}
			}

			// Token: 0x06000918 RID: 2328 RVA: 0x0002EFED File Offset: 0x0002D1ED
			public OrRestriction(params Restriction[] restrictions) : base(Restriction.ResType.Or, restrictions)
			{
			}

			// Token: 0x06000919 RID: 2329 RVA: 0x0002EFF7 File Offset: 0x0002D1F7
			internal unsafe OrRestriction(SRestriction* psres) : base(psres)
			{
			}

			// Token: 0x0600091A RID: 2330 RVA: 0x0002F000 File Offset: 0x0002D200
			internal unsafe OrRestriction(SNspiRestriction* psres) : base(psres)
			{
			}
		}

		// Token: 0x0200021A RID: 538
		public class NotRestriction : Restriction.AndOrNotRestriction
		{
			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600091B RID: 2331 RVA: 0x0002F009 File Offset: 0x0002D209
			// (set) Token: 0x0600091C RID: 2332 RVA: 0x0002F014 File Offset: 0x0002D214
			public Restriction Restriction
			{
				get
				{
					return this.restrictions[0];
				}
				set
				{
					this.restrictions = new Restriction[]
					{
						value
					};
				}
			}

			// Token: 0x0600091D RID: 2333 RVA: 0x0002F034 File Offset: 0x0002D234
			public NotRestriction(Restriction restriction) : base(Restriction.ResType.Not, new Restriction[]
			{
				restriction
			})
			{
			}

			// Token: 0x0600091E RID: 2334 RVA: 0x0002F054 File Offset: 0x0002D254
			internal unsafe NotRestriction(SRestriction* psres) : base(psres)
			{
			}

			// Token: 0x0600091F RID: 2335 RVA: 0x0002F05D File Offset: 0x0002D25D
			internal unsafe NotRestriction(SNspiRestriction* psres) : base(psres)
			{
			}
		}

		// Token: 0x0200021B RID: 539
		public class NearRestriction : Restriction
		{
			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000920 RID: 2336 RVA: 0x0002F066 File Offset: 0x0002D266
			// (set) Token: 0x06000921 RID: 2337 RVA: 0x0002F06E File Offset: 0x0002D26E
			public int Distance
			{
				get
				{
					return this.distance;
				}
				set
				{
					this.distance = value;
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x06000922 RID: 2338 RVA: 0x0002F077 File Offset: 0x0002D277
			// (set) Token: 0x06000923 RID: 2339 RVA: 0x0002F07F File Offset: 0x0002D27F
			public bool Ordered
			{
				get
				{
					return this.ordered;
				}
				set
				{
					this.ordered = value;
				}
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000924 RID: 2340 RVA: 0x0002F088 File Offset: 0x0002D288
			// (set) Token: 0x06000925 RID: 2341 RVA: 0x0002F090 File Offset: 0x0002D290
			public Restriction.AndRestriction Restriction
			{
				get
				{
					return this.restriction;
				}
				set
				{
					this.restriction = value;
				}
			}

			// Token: 0x06000926 RID: 2342 RVA: 0x0002F099 File Offset: 0x0002D299
			public NearRestriction(int distance, bool ordered, Restriction.AndRestriction restriction) : base(Microsoft.Mapi.Restriction.ResType.Near)
			{
				this.Distance = distance;
				this.Ordered = ordered;
				this.Restriction = restriction;
			}

			// Token: 0x06000927 RID: 2343 RVA: 0x0002F0B8 File Offset: 0x0002D2B8
			internal unsafe NearRestriction(SRestriction* psres) : base((Restriction.ResType)psres->rt)
			{
				this.Distance = psres->union.resNear.ulDistance;
				this.Ordered = (psres->union.resNear.ulOrdered == 1);
				this.Restriction = (Microsoft.Mapi.Restriction.Unmarshal(psres->union.resNear.lpRes) as Restriction.AndRestriction);
			}

			// Token: 0x06000928 RID: 2344 RVA: 0x0002F120 File Offset: 0x0002D320
			public override int GetBytesToMarshal()
			{
				int num = SRestriction.SizeOf + 7 & -8;
				return num + (this.restriction.GetBytesToMarshal() + 7 & -8);
			}

			// Token: 0x06000929 RID: 2345 RVA: 0x0002F14C File Offset: 0x0002D34C
			public override int GetBytesToMarshalNspi()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600092A RID: 2346 RVA: 0x0002F154 File Offset: 0x0002D354
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				SRestriction* ptr = pExtra;
				pExtra += (IntPtr)(SRestriction.SizeOf + 7 & -8);
				psr->rt = 13;
				psr->union.resNear.ulDistance = this.distance;
				psr->union.resNear.ulOrdered = (this.ordered ? 1 : 0);
				psr->union.resNear.lpRes = ptr;
				this.restriction.MarshalToNative(ptr, ref pExtra);
			}

			// Token: 0x0600092B RID: 2347 RVA: 0x0002F1CB File Offset: 0x0002D3CB
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600092C RID: 2348 RVA: 0x0002F1D2 File Offset: 0x0002D3D2
			internal override void EnumerateSubRestrictions(Restriction.EnumRestrictionDelegate del, object ctx)
			{
				this.restriction.EnumerateRestriction(del, ctx);
			}

			// Token: 0x0600092D RID: 2349 RVA: 0x0002F1E4 File Offset: 0x0002D3E4
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.NearRestriction nearRestriction = other as Restriction.NearRestriction;
				return nearRestriction != null && nearRestriction.Distance == this.Distance && nearRestriction.Ordered == this.Ordered && this.restriction.IsEqualTo(nearRestriction.Restriction);
			}

			// Token: 0x04000F9D RID: 3997
			private Restriction.AndRestriction restriction;

			// Token: 0x04000F9E RID: 3998
			private int distance;

			// Token: 0x04000F9F RID: 3999
			private bool ordered;
		}

		// Token: 0x0200021C RID: 540
		public class CountRestriction : Restriction
		{
			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x0600092E RID: 2350 RVA: 0x0002F237 File Offset: 0x0002D437
			// (set) Token: 0x0600092F RID: 2351 RVA: 0x0002F23F File Offset: 0x0002D43F
			public new int Count
			{
				get
				{
					return this.count;
				}
				set
				{
					this.count = value;
				}
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000930 RID: 2352 RVA: 0x0002F248 File Offset: 0x0002D448
			// (set) Token: 0x06000931 RID: 2353 RVA: 0x0002F250 File Offset: 0x0002D450
			public Restriction Restriction
			{
				get
				{
					return this.restriction;
				}
				set
				{
					this.restriction = value;
				}
			}

			// Token: 0x06000932 RID: 2354 RVA: 0x0002F259 File Offset: 0x0002D459
			public CountRestriction(int count, Restriction restriction) : base(Restriction.ResType.Count)
			{
				this.count = count;
				this.restriction = restriction;
			}

			// Token: 0x06000933 RID: 2355 RVA: 0x0002F271 File Offset: 0x0002D471
			internal unsafe CountRestriction(SRestriction* psres) : base(Restriction.ResType.Count)
			{
				this.count = psres->union.resCount.ulCount;
				this.restriction = Restriction.Unmarshal(psres->union.resCount.lpRes);
			}

			// Token: 0x06000934 RID: 2356 RVA: 0x0002F2AC File Offset: 0x0002D4AC
			public override int GetBytesToMarshal()
			{
				int num = SRestriction.SizeOf + 7 & -8;
				return num + (this.restriction.GetBytesToMarshal() + 7 & -8);
			}

			// Token: 0x06000935 RID: 2357 RVA: 0x0002F2D8 File Offset: 0x0002D4D8
			public override int GetBytesToMarshalNspi()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000936 RID: 2358 RVA: 0x0002F2E0 File Offset: 0x0002D4E0
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				SRestriction* ptr = pExtra;
				pExtra += (IntPtr)(SRestriction.SizeOf + 7 & -8);
				psr->rt = 11;
				psr->union.resCount.ulCount = this.count;
				psr->union.resCount.lpRes = ptr;
				this.restriction.MarshalToNative(ptr, ref pExtra);
			}

			// Token: 0x06000937 RID: 2359 RVA: 0x0002F33B File Offset: 0x0002D53B
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000938 RID: 2360 RVA: 0x0002F342 File Offset: 0x0002D542
			internal override void EnumerateSubRestrictions(Restriction.EnumRestrictionDelegate del, object ctx)
			{
				this.restriction.EnumerateRestriction(del, ctx);
			}

			// Token: 0x06000939 RID: 2361 RVA: 0x0002F354 File Offset: 0x0002D554
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.CountRestriction countRestriction = (Restriction.CountRestriction)other;
				return this.Count == countRestriction.Count && this.restriction.IsEqualTo(countRestriction.restriction);
			}

			// Token: 0x04000FA0 RID: 4000
			private int count;

			// Token: 0x04000FA1 RID: 4001
			private Restriction restriction;
		}

		// Token: 0x0200021D RID: 541
		public class PropertyRestriction : Restriction
		{
			// Token: 0x170000EA RID: 234
			// (get) Token: 0x0600093A RID: 2362 RVA: 0x0002F394 File Offset: 0x0002D594
			// (set) Token: 0x0600093B RID: 2363 RVA: 0x0002F39C File Offset: 0x0002D59C
			public Restriction.RelOp Op
			{
				get
				{
					return this.relOp;
				}
				set
				{
					this.relOp = value;
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x0600093C RID: 2364 RVA: 0x0002F3A5 File Offset: 0x0002D5A5
			// (set) Token: 0x0600093D RID: 2365 RVA: 0x0002F3B4 File Offset: 0x0002D5B4
			public PropTag PropTag
			{
				get
				{
					return this.propTag & (PropTag)4294963199U;
				}
				set
				{
					bool multiValued = this.MultiValued;
					this.propTag = value;
					this.MultiValued = multiValued;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600093E RID: 2366 RVA: 0x0002F3D6 File Offset: 0x0002D5D6
			// (set) Token: 0x0600093F RID: 2367 RVA: 0x0002F3EB File Offset: 0x0002D5EB
			public bool MultiValued
			{
				get
				{
					return (this.propTag & (PropTag)4096U) == (PropTag)4096U;
				}
				set
				{
					this.propTag = (value ? (this.propTag | (PropTag)4096U) : (this.propTag & (PropTag)4294963199U));
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x06000940 RID: 2368 RVA: 0x0002F410 File Offset: 0x0002D610
			// (set) Token: 0x06000941 RID: 2369 RVA: 0x0002F418 File Offset: 0x0002D618
			public PropValue PropValue
			{
				get
				{
					return this.propValue;
				}
				set
				{
					this.propValue = value;
				}
			}

			// Token: 0x06000942 RID: 2370 RVA: 0x0002F421 File Offset: 0x0002D621
			public PropertyRestriction(Restriction.RelOp relOp, PropTag tag, object value) : this(relOp, tag, false, new PropValue(tag, value))
			{
			}

			// Token: 0x06000943 RID: 2371 RVA: 0x0002F433 File Offset: 0x0002D633
			public PropertyRestriction(Restriction.RelOp relOp, PropTag tag, bool multiValued, object value) : this(relOp, tag, multiValued, new PropValue(tag, value))
			{
			}

			// Token: 0x06000944 RID: 2372 RVA: 0x0002F446 File Offset: 0x0002D646
			public PropertyRestriction(Restriction.RelOp relOp, PropTag tag, PropValue value) : this(relOp, tag, false, value)
			{
			}

			// Token: 0x06000945 RID: 2373 RVA: 0x0002F452 File Offset: 0x0002D652
			public PropertyRestriction(Restriction.RelOp relOp, PropTag tag, bool multiValued, PropValue value) : base(Restriction.ResType.Property)
			{
				this.relOp = relOp;
				this.propTag = tag;
				this.propValue = value;
				this.MultiValued = multiValued;
			}

			// Token: 0x06000946 RID: 2374 RVA: 0x0002F478 File Offset: 0x0002D678
			internal unsafe PropertyRestriction(SRestriction* psres) : base(Restriction.ResType.Property)
			{
				this.relOp = (Restriction.RelOp)psres->union.resProperty.relop;
				this.propTag = (PropTag)psres->union.resProperty.ulPropTag;
				this.propValue = new PropValue(psres->union.resProperty.lpProp);
			}

			// Token: 0x06000947 RID: 2375 RVA: 0x0002F4D4 File Offset: 0x0002D6D4
			internal unsafe PropertyRestriction(SNspiRestriction* psres) : base(Restriction.ResType.Property)
			{
				this.relOp = (Restriction.RelOp)psres->union.resProperty.relop;
				this.propTag = (PropTag)psres->union.resProperty.ulPropTag;
				this.propValue = new PropValue(psres->union.resProperty.lpProp, true);
			}

			// Token: 0x06000948 RID: 2376 RVA: 0x0002F530 File Offset: 0x0002D730
			public override int GetBytesToMarshal()
			{
				return (SRestriction.SizeOf + 7 & -8) + (this.propValue.GetBytesToMarshal() + 7 & -8);
			}

			// Token: 0x06000949 RID: 2377 RVA: 0x0002F54D File Offset: 0x0002D74D
			public override int GetBytesToMarshalNspi()
			{
				return (SNspiRestriction.SizeOf + 7 & -8) + (this.propValue.GetBytesToMarshal() + 7 & -8);
			}

			// Token: 0x0600094A RID: 2378 RVA: 0x0002F56C File Offset: 0x0002D76C
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				SPropValue* ptr = pExtra;
				pExtra += (IntPtr)(SPropValue.SizeOf + 7 & -8);
				psr->rt = 4;
				psr->union.resProperty.relop = (int)this.relOp;
				psr->union.resProperty.ulPropTag = (int)this.propTag;
				psr->union.resProperty.lpProp = ptr;
				this.propValue.MarshalToNative(ptr, ref pExtra);
			}

			// Token: 0x0600094B RID: 2379 RVA: 0x0002F5DC File Offset: 0x0002D7DC
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				SPropValue* ptr = pExtra;
				pExtra += (IntPtr)(SPropValue.SizeOf + 7 & -8);
				psr->rt = 4;
				psr->union.resProperty.relop = (int)this.relOp;
				psr->union.resProperty.ulPropTag = (int)this.propTag;
				psr->union.resProperty.lpProp = ptr;
				this.propValue.MarshalToNative(ptr, ref pExtra);
			}

			// Token: 0x0600094C RID: 2380 RVA: 0x0002F64C File Offset: 0x0002D84C
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.PropertyRestriction propertyRestriction = (Restriction.PropertyRestriction)other;
				return this.Op == propertyRestriction.Op && this.PropTag == propertyRestriction.PropTag && this.PropValue.IsEqualTo(propertyRestriction.PropValue);
			}

			// Token: 0x04000FA2 RID: 4002
			private Restriction.RelOp relOp;

			// Token: 0x04000FA3 RID: 4003
			private PropValue propValue;

			// Token: 0x04000FA4 RID: 4004
			private PropTag propTag;
		}

		// Token: 0x0200021E RID: 542
		public class ContentRestriction : Restriction
		{
			// Token: 0x170000EE RID: 238
			// (get) Token: 0x0600094D RID: 2381 RVA: 0x0002F6A0 File Offset: 0x0002D8A0
			// (set) Token: 0x0600094E RID: 2382 RVA: 0x0002F6A8 File Offset: 0x0002D8A8
			public ContentFlags Flags
			{
				get
				{
					return this.contentFlags;
				}
				set
				{
					this.contentFlags = value;
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x0600094F RID: 2383 RVA: 0x0002F6B1 File Offset: 0x0002D8B1
			// (set) Token: 0x06000950 RID: 2384 RVA: 0x0002F6C0 File Offset: 0x0002D8C0
			public PropTag PropTag
			{
				get
				{
					return this.propTag & (PropTag)4294963199U;
				}
				set
				{
					bool multiValued = this.MultiValued;
					this.propTag = value;
					this.MultiValued = multiValued;
				}
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000951 RID: 2385 RVA: 0x0002F6E2 File Offset: 0x0002D8E2
			// (set) Token: 0x06000952 RID: 2386 RVA: 0x0002F6EA File Offset: 0x0002D8EA
			public PropValue PropValue
			{
				get
				{
					return this.propValue;
				}
				set
				{
					this.propValue = value;
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000953 RID: 2387 RVA: 0x0002F6F3 File Offset: 0x0002D8F3
			// (set) Token: 0x06000954 RID: 2388 RVA: 0x0002F708 File Offset: 0x0002D908
			public bool MultiValued
			{
				get
				{
					return (this.propTag & (PropTag)4096U) == (PropTag)4096U;
				}
				set
				{
					this.propTag = (value ? (this.propTag | (PropTag)4096U) : (this.propTag & (PropTag)4294963199U));
				}
			}

			// Token: 0x06000955 RID: 2389 RVA: 0x0002F72D File Offset: 0x0002D92D
			public ContentRestriction(PropTag tag, object value, ContentFlags flags) : this(tag, false, new PropValue(tag, value), flags)
			{
			}

			// Token: 0x06000956 RID: 2390 RVA: 0x0002F73F File Offset: 0x0002D93F
			public ContentRestriction(PropTag tag, bool multiValued, object value, ContentFlags flags) : this(tag, multiValued, new PropValue(tag, value), flags)
			{
			}

			// Token: 0x06000957 RID: 2391 RVA: 0x0002F752 File Offset: 0x0002D952
			public ContentRestriction(PropTag tag, bool multiValued, PropValue value, ContentFlags flags) : base(Restriction.ResType.Content)
			{
				this.contentFlags = flags;
				this.propTag = tag;
				this.propValue = value;
				this.MultiValued = multiValued;
			}

			// Token: 0x06000958 RID: 2392 RVA: 0x0002F778 File Offset: 0x0002D978
			internal unsafe ContentRestriction(SRestriction* psres) : base(Restriction.ResType.Content)
			{
				this.contentFlags = (ContentFlags)psres->union.resContent.ulFuzzyLevel;
				this.propValue = new PropValue(psres->union.resContent.lpProp);
				this.propTag = (PropTag)psres->union.resContent.ulPropTag;
			}

			// Token: 0x06000959 RID: 2393 RVA: 0x0002F7D4 File Offset: 0x0002D9D4
			internal unsafe ContentRestriction(SNspiRestriction* psres) : base(Restriction.ResType.Content)
			{
				this.contentFlags = (ContentFlags)psres->union.resContent.ulFuzzyLevel;
				this.propValue = new PropValue(psres->union.resContent.lpProp, true);
				this.propTag = (PropTag)psres->union.resContent.ulPropTag;
			}

			// Token: 0x0600095A RID: 2394 RVA: 0x0002F830 File Offset: 0x0002DA30
			public override int GetBytesToMarshal()
			{
				return (SRestriction.SizeOf + 7 & -8) + (this.propValue.GetBytesToMarshal() + 7 & -8);
			}

			// Token: 0x0600095B RID: 2395 RVA: 0x0002F84D File Offset: 0x0002DA4D
			public override int GetBytesToMarshalNspi()
			{
				return (SNspiRestriction.SizeOf + 7 & -8) + (this.propValue.GetBytesToMarshal() + 7 & -8);
			}

			// Token: 0x0600095C RID: 2396 RVA: 0x0002F86C File Offset: 0x0002DA6C
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				SPropValue* ptr = pExtra;
				pExtra += (IntPtr)(SPropValue.SizeOf + 7 & -8);
				psr->rt = 3;
				psr->union.resContent.ulFuzzyLevel = (int)this.contentFlags;
				psr->union.resContent.ulPropTag = (int)this.propTag;
				psr->union.resContent.lpProp = ptr;
				this.propValue.MarshalToNative(ptr, ref pExtra);
			}

			// Token: 0x0600095D RID: 2397 RVA: 0x0002F8DC File Offset: 0x0002DADC
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				SPropValue* ptr = pExtra;
				pExtra += (IntPtr)(SPropValue.SizeOf + 7 & -8);
				psr->rt = 3;
				psr->union.resContent.ulFuzzyLevel = (int)this.contentFlags;
				psr->union.resContent.ulPropTag = (int)this.propTag;
				psr->union.resContent.lpProp = ptr;
				this.propValue.MarshalToNative(ptr, ref pExtra);
			}

			// Token: 0x0600095E RID: 2398 RVA: 0x0002F94C File Offset: 0x0002DB4C
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.ContentRestriction contentRestriction = (Restriction.ContentRestriction)other;
				return this.Flags == contentRestriction.Flags && this.PropTag == contentRestriction.PropTag && this.MultiValued == contentRestriction.MultiValued && this.PropValue.IsEqualTo(contentRestriction.PropValue);
			}

			// Token: 0x04000FA5 RID: 4005
			private ContentFlags contentFlags;

			// Token: 0x04000FA6 RID: 4006
			private PropValue propValue;

			// Token: 0x04000FA7 RID: 4007
			private PropTag propTag;
		}

		// Token: 0x0200021F RID: 543
		public class BitMaskRestriction : Restriction
		{
			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x0600095F RID: 2399 RVA: 0x0002F9AE File Offset: 0x0002DBAE
			// (set) Token: 0x06000960 RID: 2400 RVA: 0x0002F9B6 File Offset: 0x0002DBB6
			public PropTag Tag
			{
				get
				{
					return this.tag;
				}
				set
				{
					this.tag = value;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000961 RID: 2401 RVA: 0x0002F9BF File Offset: 0x0002DBBF
			// (set) Token: 0x06000962 RID: 2402 RVA: 0x0002F9C7 File Offset: 0x0002DBC7
			public Restriction.RelBmr Bmr
			{
				get
				{
					return this.relbmr;
				}
				set
				{
					this.relbmr = value;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000963 RID: 2403 RVA: 0x0002F9D0 File Offset: 0x0002DBD0
			// (set) Token: 0x06000964 RID: 2404 RVA: 0x0002F9D8 File Offset: 0x0002DBD8
			public int Mask
			{
				get
				{
					return this.mask;
				}
				set
				{
					this.mask = value;
				}
			}

			// Token: 0x06000965 RID: 2405 RVA: 0x0002F9E1 File Offset: 0x0002DBE1
			public BitMaskRestriction(Restriction.RelBmr relbmr, PropTag tag, int mask) : base(Restriction.ResType.BitMask)
			{
				this.relbmr = relbmr;
				this.tag = tag;
				this.mask = mask;
			}

			// Token: 0x06000966 RID: 2406 RVA: 0x0002FA00 File Offset: 0x0002DC00
			internal unsafe BitMaskRestriction(SRestriction* psres) : base(Restriction.ResType.BitMask)
			{
				this.mask = psres->union.resBitMask.ulMask;
				this.tag = (PropTag)psres->union.resBitMask.ulPropTag;
				this.relbmr = (Restriction.RelBmr)psres->union.resBitMask.relBMR;
			}

			// Token: 0x06000967 RID: 2407 RVA: 0x0002FA56 File Offset: 0x0002DC56
			public override int GetBytesToMarshal()
			{
				return SRestriction.SizeOf + 7 & -8;
			}

			// Token: 0x06000968 RID: 2408 RVA: 0x0002FA62 File Offset: 0x0002DC62
			public override int GetBytesToMarshalNspi()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000969 RID: 2409 RVA: 0x0002FA6C File Offset: 0x0002DC6C
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				psr->rt = 6;
				psr->union.resBitMask.relBMR = (int)this.relbmr;
				psr->union.resBitMask.ulPropTag = (int)this.tag;
				psr->union.resBitMask.ulMask = this.mask;
			}

			// Token: 0x0600096A RID: 2410 RVA: 0x0002FAC2 File Offset: 0x0002DCC2
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600096B RID: 2411 RVA: 0x0002FACC File Offset: 0x0002DCCC
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.BitMaskRestriction bitMaskRestriction = (Restriction.BitMaskRestriction)other;
				return this.Bmr == bitMaskRestriction.Bmr && this.Tag == bitMaskRestriction.Tag && this.Mask == bitMaskRestriction.Mask;
			}

			// Token: 0x04000FA8 RID: 4008
			private Restriction.RelBmr relbmr;

			// Token: 0x04000FA9 RID: 4009
			private PropTag tag;

			// Token: 0x04000FAA RID: 4010
			private int mask;
		}

		// Token: 0x02000220 RID: 544
		public class ComparePropertyRestriction : Restriction
		{
			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600096C RID: 2412 RVA: 0x0002FB18 File Offset: 0x0002DD18
			// (set) Token: 0x0600096D RID: 2413 RVA: 0x0002FB20 File Offset: 0x0002DD20
			public PropTag TagLeft
			{
				get
				{
					return this.tagLeft;
				}
				set
				{
					this.tagLeft = value;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x0600096E RID: 2414 RVA: 0x0002FB29 File Offset: 0x0002DD29
			// (set) Token: 0x0600096F RID: 2415 RVA: 0x0002FB31 File Offset: 0x0002DD31
			public PropTag TagRight
			{
				get
				{
					return this.tagRight;
				}
				set
				{
					this.tagRight = value;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000970 RID: 2416 RVA: 0x0002FB3A File Offset: 0x0002DD3A
			// (set) Token: 0x06000971 RID: 2417 RVA: 0x0002FB42 File Offset: 0x0002DD42
			public Restriction.RelOp Op
			{
				get
				{
					return this.relOp;
				}
				set
				{
					this.relOp = value;
				}
			}

			// Token: 0x06000972 RID: 2418 RVA: 0x0002FB4B File Offset: 0x0002DD4B
			public ComparePropertyRestriction(Restriction.RelOp relOp, PropTag tagLeft, PropTag tagRight) : base(Restriction.ResType.CompareProps)
			{
				this.relOp = relOp;
				this.tagLeft = tagLeft;
				this.tagRight = tagRight;
			}

			// Token: 0x06000973 RID: 2419 RVA: 0x0002FB6C File Offset: 0x0002DD6C
			internal unsafe ComparePropertyRestriction(SRestriction* psres) : base(Restriction.ResType.CompareProps)
			{
				this.relOp = (Restriction.RelOp)psres->union.resCompareProps.relop;
				this.tagLeft = (PropTag)psres->union.resCompareProps.ulPropTag1;
				this.tagRight = (PropTag)psres->union.resCompareProps.ulPropTag2;
			}

			// Token: 0x06000974 RID: 2420 RVA: 0x0002FBC2 File Offset: 0x0002DDC2
			public override int GetBytesToMarshal()
			{
				return SRestriction.SizeOf + 7 & -8;
			}

			// Token: 0x06000975 RID: 2421 RVA: 0x0002FBCE File Offset: 0x0002DDCE
			public override int GetBytesToMarshalNspi()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000976 RID: 2422 RVA: 0x0002FBD8 File Offset: 0x0002DDD8
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				psr->rt = 5;
				psr->union.resCompareProps.relop = (int)this.relOp;
				psr->union.resCompareProps.ulPropTag1 = (int)this.tagLeft;
				psr->union.resCompareProps.ulPropTag2 = (int)this.tagRight;
			}

			// Token: 0x06000977 RID: 2423 RVA: 0x0002FC2E File Offset: 0x0002DE2E
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000978 RID: 2424 RVA: 0x0002FC38 File Offset: 0x0002DE38
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.ComparePropertyRestriction comparePropertyRestriction = (Restriction.ComparePropertyRestriction)other;
				return this.TagLeft == comparePropertyRestriction.TagLeft && this.TagRight == comparePropertyRestriction.TagRight && this.Op == comparePropertyRestriction.Op;
			}

			// Token: 0x04000FAB RID: 4011
			private Restriction.RelOp relOp;

			// Token: 0x04000FAC RID: 4012
			private PropTag tagLeft;

			// Token: 0x04000FAD RID: 4013
			private PropTag tagRight;
		}

		// Token: 0x02000221 RID: 545
		public class ExistRestriction : Restriction
		{
			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000979 RID: 2425 RVA: 0x0002FC84 File Offset: 0x0002DE84
			// (set) Token: 0x0600097A RID: 2426 RVA: 0x0002FC8C File Offset: 0x0002DE8C
			public PropTag Tag
			{
				get
				{
					return this.tag;
				}
				set
				{
					this.tag = value;
				}
			}

			// Token: 0x0600097B RID: 2427 RVA: 0x0002FC95 File Offset: 0x0002DE95
			public ExistRestriction(PropTag tag) : base(Restriction.ResType.Exist)
			{
				this.tag = tag;
			}

			// Token: 0x0600097C RID: 2428 RVA: 0x0002FCA5 File Offset: 0x0002DEA5
			internal unsafe ExistRestriction(SRestriction* psres) : base(Restriction.ResType.Exist)
			{
				this.tag = (PropTag)psres->union.resExist.ulPropTag;
			}

			// Token: 0x0600097D RID: 2429 RVA: 0x0002FCC4 File Offset: 0x0002DEC4
			internal unsafe ExistRestriction(SNspiRestriction* psres) : base(Restriction.ResType.Exist)
			{
				this.tag = (PropTag)psres->union.resExist.ulPropTag;
			}

			// Token: 0x0600097E RID: 2430 RVA: 0x0002FCE3 File Offset: 0x0002DEE3
			public override int GetBytesToMarshal()
			{
				return SRestriction.SizeOf + 7 & -8;
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x0002FCEF File Offset: 0x0002DEEF
			public override int GetBytesToMarshalNspi()
			{
				return SNspiRestriction.SizeOf + 7 & -8;
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x0002FCFC File Offset: 0x0002DEFC
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				psr->rt = 8;
				psr->union.resExist.ulPropTag = (int)this.tag;
				psr->union.resExist.ulReserved1 = 0;
				psr->union.resExist.ulReserved2 = 0;
			}

			// Token: 0x06000981 RID: 2433 RVA: 0x0002FD48 File Offset: 0x0002DF48
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				psr->rt = 8;
				psr->union.resExist.ulPropTag = (int)this.tag;
				psr->union.resExist.ulReserved1 = 0;
				psr->union.resExist.ulReserved2 = 0;
			}

			// Token: 0x06000982 RID: 2434 RVA: 0x0002FD94 File Offset: 0x0002DF94
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.ExistRestriction existRestriction = (Restriction.ExistRestriction)other;
				return this.Tag == existRestriction.Tag;
			}

			// Token: 0x04000FAE RID: 4014
			private PropTag tag;
		}

		// Token: 0x02000222 RID: 546
		public class SizeRestriction : Restriction
		{
			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000983 RID: 2435 RVA: 0x0002FDC4 File Offset: 0x0002DFC4
			// (set) Token: 0x06000984 RID: 2436 RVA: 0x0002FDCC File Offset: 0x0002DFCC
			public Restriction.RelOp Op
			{
				get
				{
					return this.relop;
				}
				set
				{
					this.relop = value;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000985 RID: 2437 RVA: 0x0002FDD5 File Offset: 0x0002DFD5
			// (set) Token: 0x06000986 RID: 2438 RVA: 0x0002FDDD File Offset: 0x0002DFDD
			public PropTag Tag
			{
				get
				{
					return this.tag;
				}
				set
				{
					this.tag = value;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000987 RID: 2439 RVA: 0x0002FDE6 File Offset: 0x0002DFE6
			// (set) Token: 0x06000988 RID: 2440 RVA: 0x0002FDEE File Offset: 0x0002DFEE
			public int Size
			{
				get
				{
					return this.size;
				}
				set
				{
					this.size = value;
				}
			}

			// Token: 0x06000989 RID: 2441 RVA: 0x0002FDF7 File Offset: 0x0002DFF7
			public SizeRestriction(Restriction.RelOp relop, PropTag tag, int size) : base(Restriction.ResType.Size)
			{
				this.relop = relop;
				this.tag = tag;
				this.size = size;
			}

			// Token: 0x0600098A RID: 2442 RVA: 0x0002FE18 File Offset: 0x0002E018
			internal unsafe SizeRestriction(SRestriction* psres) : base(Restriction.ResType.Size)
			{
				this.relop = (Restriction.RelOp)psres->union.resSize.relop;
				this.tag = (PropTag)psres->union.resSize.ulPropTag;
				this.size = psres->union.resSize.cb;
			}

			// Token: 0x0600098B RID: 2443 RVA: 0x0002FE6E File Offset: 0x0002E06E
			public override int GetBytesToMarshal()
			{
				return SRestriction.SizeOf + 7 & -8;
			}

			// Token: 0x0600098C RID: 2444 RVA: 0x0002FE7A File Offset: 0x0002E07A
			public override int GetBytesToMarshalNspi()
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600098D RID: 2445 RVA: 0x0002FE84 File Offset: 0x0002E084
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				psr->rt = 7;
				psr->union.resSize.relop = (int)this.relop;
				psr->union.resSize.ulPropTag = (int)this.tag;
				psr->union.resSize.cb = this.size;
			}

			// Token: 0x0600098E RID: 2446 RVA: 0x0002FEDA File Offset: 0x0002E0DA
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600098F RID: 2447 RVA: 0x0002FEE4 File Offset: 0x0002E0E4
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.SizeRestriction sizeRestriction = (Restriction.SizeRestriction)other;
				return this.Tag == sizeRestriction.Tag && this.Op == sizeRestriction.Op && this.Size == sizeRestriction.Size;
			}

			// Token: 0x04000FAF RID: 4015
			private Restriction.RelOp relop;

			// Token: 0x04000FB0 RID: 4016
			private PropTag tag;

			// Token: 0x04000FB1 RID: 4017
			private int size;
		}

		// Token: 0x02000223 RID: 547
		public class SubRestriction : Restriction
		{
			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002FF30 File Offset: 0x0002E130
			// (set) Token: 0x06000991 RID: 2449 RVA: 0x0002FF38 File Offset: 0x0002E138
			public Restriction Restriction
			{
				get
				{
					return this.restriction;
				}
				set
				{
					this.restriction = value;
				}
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x0002FF41 File Offset: 0x0002E141
			internal SubRestriction(PropTag tag, Restriction restriction) : base(Restriction.ResType.SubRestriction)
			{
				this.tag = tag;
				this.restriction = restriction;
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x0002FF59 File Offset: 0x0002E159
			internal unsafe SubRestriction(SRestriction* psres) : base(Restriction.ResType.SubRestriction)
			{
				this.tag = (PropTag)psres->union.resSub.ulSubObject;
				this.restriction = Restriction.Unmarshal(psres->union.resSub.lpRes);
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x0002FF94 File Offset: 0x0002E194
			public override int GetBytesToMarshal()
			{
				int num = SRestriction.SizeOf + 7 & -8;
				return num + (this.restriction.GetBytesToMarshal() + 7 & -8);
			}

			// Token: 0x06000995 RID: 2453 RVA: 0x0002FFC0 File Offset: 0x0002E1C0
			public override int GetBytesToMarshalNspi()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000996 RID: 2454 RVA: 0x0002FFC8 File Offset: 0x0002E1C8
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				SRestriction* ptr = pExtra;
				pExtra += (IntPtr)(SRestriction.SizeOf + 7 & -8);
				psr->rt = 9;
				psr->union.resSub.ulSubObject = (int)this.tag;
				psr->union.resSub.lpRes = ptr;
				this.restriction.MarshalToNative(ptr, ref pExtra);
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x00030023 File Offset: 0x0002E223
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x0003002A File Offset: 0x0002E22A
			internal override void EnumerateSubRestrictions(Restriction.EnumRestrictionDelegate del, object ctx)
			{
				this.restriction.EnumerateRestriction(del, ctx);
			}

			// Token: 0x06000999 RID: 2457 RVA: 0x0003003C File Offset: 0x0002E23C
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.SubRestriction subRestriction = (Restriction.SubRestriction)other;
				return this.tag == subRestriction.tag && this.restriction.IsEqualTo(subRestriction.restriction);
			}

			// Token: 0x04000FB2 RID: 4018
			private PropTag tag;

			// Token: 0x04000FB3 RID: 4019
			private Restriction restriction;
		}

		// Token: 0x02000224 RID: 548
		public class AttachmentRestriction : Restriction.SubRestriction
		{
			// Token: 0x0600099A RID: 2458 RVA: 0x0003007C File Offset: 0x0002E27C
			public AttachmentRestriction(Restriction restriction) : base(PropTag.MessageAttachments, restriction)
			{
			}

			// Token: 0x0600099B RID: 2459 RVA: 0x0003008A File Offset: 0x0002E28A
			internal unsafe AttachmentRestriction(SRestriction* psres) : base(psres)
			{
			}
		}

		// Token: 0x02000225 RID: 549
		public class RecipientRestriction : Restriction.SubRestriction
		{
			// Token: 0x0600099C RID: 2460 RVA: 0x00030093 File Offset: 0x0002E293
			public RecipientRestriction(Restriction restriction) : base(PropTag.MessageRecipients, restriction)
			{
			}

			// Token: 0x0600099D RID: 2461 RVA: 0x000300A1 File Offset: 0x0002E2A1
			internal unsafe RecipientRestriction(SRestriction* psres) : base(psres)
			{
			}
		}

		// Token: 0x02000226 RID: 550
		public class CommentRestriction : Restriction
		{
			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600099E RID: 2462 RVA: 0x000300AA File Offset: 0x0002E2AA
			// (set) Token: 0x0600099F RID: 2463 RVA: 0x000300B2 File Offset: 0x0002E2B2
			public PropValue[] Values
			{
				get
				{
					return this.propValues;
				}
				set
				{
					this.propValues = value;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x060009A0 RID: 2464 RVA: 0x000300BB File Offset: 0x0002E2BB
			// (set) Token: 0x060009A1 RID: 2465 RVA: 0x000300C3 File Offset: 0x0002E2C3
			public Restriction Restriction
			{
				get
				{
					return this.restriction;
				}
				set
				{
					this.restriction = value;
				}
			}

			// Token: 0x060009A2 RID: 2466 RVA: 0x000300CC File Offset: 0x0002E2CC
			public CommentRestriction(Restriction restriction, PropValue[] propValues) : base(Restriction.ResType.Comment)
			{
				this.propValues = propValues;
				this.restriction = restriction;
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x000300E4 File Offset: 0x0002E2E4
			internal unsafe CommentRestriction(SRestriction* psres) : base(Restriction.ResType.Comment)
			{
				this.restriction = Restriction.Unmarshal(psres->union.resComment.lpRes);
				this.propValues = new PropValue[psres->union.resComment.cValues];
				for (int i = 0; i < psres->union.resComment.cValues; i++)
				{
					this.propValues[i] = new PropValue(psres->union.resComment.lpProp + i);
				}
			}

			// Token: 0x060009A4 RID: 2468 RVA: 0x0003017C File Offset: 0x0002E37C
			public override int GetBytesToMarshal()
			{
				int num = SRestriction.SizeOf + 7 & -8;
				if (this.propValues != null && this.propValues.Length > 0)
				{
					foreach (PropValue propValue in this.propValues)
					{
						num += (propValue.GetBytesToMarshal() + 7 & -8);
					}
				}
				if (this.restriction != null)
				{
					num += (this.restriction.GetBytesToMarshal() + 7 & -8);
				}
				return num;
			}

			// Token: 0x060009A5 RID: 2469 RVA: 0x000301F4 File Offset: 0x0002E3F4
			public override int GetBytesToMarshalNspi()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060009A6 RID: 2470 RVA: 0x000301FC File Offset: 0x0002E3FC
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				psr->rt = 10;
				if (this.restriction != null)
				{
					SRestriction* ptr = pExtra;
					pExtra += (IntPtr)(SRestriction.SizeOf + 7 & -8);
					psr->union.resComment.lpRes = ptr;
					this.restriction.MarshalToNative(ptr, ref pExtra);
				}
				else
				{
					psr->union.resComment.lpRes = null;
				}
				if (this.propValues != null && this.propValues.Length > 0)
				{
					SPropValue* lpProp = pExtra;
					pExtra += (IntPtr)(Marshal.SizeOf(typeof(SPropValue)) * this.propValues.Length + 7 & -8);
					psr->union.resComment.cValues = this.propValues.Length;
					psr->union.resComment.lpProp = lpProp;
					foreach (PropValue propValue in this.propValues)
					{
						propValue.MarshalToNative(lpProp++, ref pExtra);
					}
					return;
				}
				psr->union.resComment.cValues = 0;
				psr->union.resComment.lpProp = null;
			}

			// Token: 0x060009A7 RID: 2471 RVA: 0x00030321 File Offset: 0x0002E521
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060009A8 RID: 2472 RVA: 0x00030328 File Offset: 0x0002E528
			internal override void EnumerateSubRestrictions(Restriction.EnumRestrictionDelegate del, object ctx)
			{
				this.restriction.EnumerateRestriction(del, ctx);
			}

			// Token: 0x060009A9 RID: 2473 RVA: 0x00030338 File Offset: 0x0002E538
			internal override bool IsEqualTo(Restriction other)
			{
				if (!base.IsEqualTo(other))
				{
					return false;
				}
				Restriction.CommentRestriction commentRestriction = (Restriction.CommentRestriction)other;
				if (this.Values != null != (commentRestriction.Values != null))
				{
					return false;
				}
				if (this.Values != null)
				{
					if (commentRestriction.Values == null || commentRestriction.Values.Length != this.Values.Length)
					{
						return false;
					}
					for (int i = 0; i < this.Values.Length; i++)
					{
						if (!this.Values[i].IsEqualTo(commentRestriction.Values[i]))
						{
							return false;
						}
					}
				}
				else if (commentRestriction.Values != null)
				{
					return false;
				}
				return this.restriction.IsEqualTo(commentRestriction.restriction);
			}

			// Token: 0x04000FB4 RID: 4020
			private PropValue[] propValues;

			// Token: 0x04000FB5 RID: 4021
			private Restriction restriction;
		}

		// Token: 0x02000227 RID: 551
		public class TrueRestriction : Restriction
		{
			// Token: 0x060009AA RID: 2474 RVA: 0x000303ED File Offset: 0x0002E5ED
			public TrueRestriction() : base(Restriction.ResType.True)
			{
			}

			// Token: 0x060009AB RID: 2475 RVA: 0x000303FA File Offset: 0x0002E5FA
			internal unsafe TrueRestriction(SRestriction* psres) : base(Restriction.ResType.True)
			{
			}

			// Token: 0x060009AC RID: 2476 RVA: 0x00030407 File Offset: 0x0002E607
			public override int GetBytesToMarshal()
			{
				return SRestriction.SizeOf + 7 & -8;
			}

			// Token: 0x060009AD RID: 2477 RVA: 0x00030413 File Offset: 0x0002E613
			public override int GetBytesToMarshalNspi()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060009AE RID: 2478 RVA: 0x0003041A File Offset: 0x0002E61A
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				psr->rt = 131;
			}

			// Token: 0x060009AF RID: 2479 RVA: 0x00030427 File Offset: 0x0002E627
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x02000228 RID: 552
		public class FalseRestriction : Restriction
		{
			// Token: 0x060009B0 RID: 2480 RVA: 0x0003042E File Offset: 0x0002E62E
			public FalseRestriction() : base(Restriction.ResType.False)
			{
			}

			// Token: 0x060009B1 RID: 2481 RVA: 0x0003043B File Offset: 0x0002E63B
			internal unsafe FalseRestriction(SRestriction* psres) : base(Restriction.ResType.False)
			{
			}

			// Token: 0x060009B2 RID: 2482 RVA: 0x00030448 File Offset: 0x0002E648
			public override int GetBytesToMarshal()
			{
				return SRestriction.SizeOf + 7 & -8;
			}

			// Token: 0x060009B3 RID: 2483 RVA: 0x00030454 File Offset: 0x0002E654
			public override int GetBytesToMarshalNspi()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060009B4 RID: 2484 RVA: 0x0003045B File Offset: 0x0002E65B
			internal unsafe override void MarshalToNative(SRestriction* psr, ref byte* pExtra)
			{
				psr->rt = 132;
			}

			// Token: 0x060009B5 RID: 2485 RVA: 0x00030468 File Offset: 0x0002E668
			internal unsafe override void MarshalToNative(SNspiRestriction* psr, ref byte* pExtra)
			{
				throw new NotSupportedException();
			}
		}
	}
}
