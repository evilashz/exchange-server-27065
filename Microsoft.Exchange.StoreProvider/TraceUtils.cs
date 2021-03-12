using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Mapi
{
	// Token: 0x02000254 RID: 596
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TraceUtils : ComponentTrace<MapiNetTags>
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x0003343C File Offset: 0x0003163C
		public static string MakeString(object someObject)
		{
			if (someObject == null)
			{
				return "null";
			}
			if (someObject is string)
			{
				return "\"" + someObject + "\"";
			}
			return someObject.ToString();
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00033468 File Offset: 0x00031668
		public static string MakeHash(object someObject)
		{
			if (someObject != null)
			{
				return someObject.GetHashCode().ToString() + "(hash)";
			}
			return "null";
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00033498 File Offset: 0x00031698
		public static string DumpArray(Array someArray)
		{
			if (someArray != null)
			{
				return "[..., array_len=" + someArray.Length.ToString() + "]";
			}
			return "null";
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x000334CC File Offset: 0x000316CC
		public static string DumpCollection<T>(ICollection<T> collection)
		{
			if (collection != null)
			{
				return "[..., collection_len=" + collection.Count.ToString() + "]";
			}
			return "null";
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000334FF File Offset: 0x000316FF
		public static string DumpEntryId(byte[] entryId)
		{
			if (entryId == null)
			{
				return "null";
			}
			return string.Format("[len={0}, data={1}]", entryId.GetLength(0), TraceUtils.DumpBytes(entryId));
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00033528 File Offset: 0x00031728
		public static string DumpBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				stringBuilder.Append(b.ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00033574 File Offset: 0x00031774
		public static string DumpMvString(string[] strings)
		{
			if (strings == null)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string str in strings)
			{
				stringBuilder.Append(str + "; ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000335BC File Offset: 0x000317BC
		public static string DumpPropTag(PropTag ptag)
		{
			if (ptag < PropTag.Null)
			{
				return string.Format("0x{0:x}(NamedProp)", (int)ptag);
			}
			if (EnumValidator<PropTag>.IsValidValue(ptag))
			{
				return string.Format("0x{0:x}({1})", (int)ptag, ptag);
			}
			return string.Format("0x{0:x}", (int)ptag);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0003360F File Offset: 0x0003180F
		public static string DumpPropTagsArray(ICollection<PropTag> ptagsArray)
		{
			return TraceUtils.DumpCollection<PropTag>(ptagsArray);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00033617 File Offset: 0x00031817
		public static string DumpNamedPropsArray(ICollection<NamedProp> namedPropsArray)
		{
			return TraceUtils.DumpCollection<NamedProp>(namedPropsArray);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00033620 File Offset: 0x00031820
		public static string DumpPropVal(PropValue propVal)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[Tag:{0}, Value:", TraceUtils.DumpPropTag(propVal.PropTag));
			if (propVal.RawValue == null)
			{
				stringBuilder.Append("null");
			}
			else if (propVal.IsError())
			{
				stringBuilder.AppendFormat("{0}(error)", propVal.GetErrorValue());
			}
			else if (propVal.Value is short)
			{
				stringBuilder.AppendFormat("{0}(short)", propVal.Value.ToString());
			}
			else if (propVal.Value is int)
			{
				stringBuilder.AppendFormat("{0}(int)", propVal.Value.ToString());
			}
			else if (propVal.Value is float)
			{
				stringBuilder.AppendFormat("{0}(float)", propVal.Value.ToString());
			}
			else if (propVal.Value is double)
			{
				stringBuilder.AppendFormat("{0}(double)", propVal.Value.ToString());
			}
			else if (propVal.Value is bool)
			{
				stringBuilder.AppendFormat("{0}(bool)", propVal.Value.ToString());
			}
			else if (propVal.Value is long)
			{
				stringBuilder.AppendFormat("{0}(long)", propVal.Value.ToString());
			}
			else if (propVal.Value is string)
			{
				stringBuilder.AppendFormat("\"{0}\"(string)", propVal.Value);
			}
			else if (propVal.Value is Guid)
			{
				stringBuilder.AppendFormat("{0}(Guid)", propVal.Value.ToString());
			}
			else if (propVal.Value is byte[])
			{
				stringBuilder.AppendFormat("cb = {0}, lpb = {1}", propVal.GetBytes().GetLength(0), TraceUtils.DumpBytes(propVal.GetBytes()));
			}
			else if (propVal.Value is string[])
			{
				stringBuilder.AppendFormat("MvString: cValues = {0}. Values = {1}", propVal.GetStringArray().GetLength(0), TraceUtils.DumpMvString(propVal.GetStringArray()));
			}
			else if (propVal.Value != null)
			{
				stringBuilder.AppendFormat("({0})", propVal.Value.ToString());
			}
			else
			{
				stringBuilder.AppendFormat("({0})", propVal.RawValue.GetType().ToString());
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000338A4 File Offset: 0x00031AA4
		public static string DumpPropValsArray(ICollection<PropValue> propValsArray)
		{
			return TraceUtils.DumpCollection<PropValue>(propValsArray);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000338AC File Offset: 0x00031AAC
		public static string DumpPropProblemsArray(PropProblem[] propProblemsArray)
		{
			return TraceUtils.DumpArray(propProblemsArray);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000338B4 File Offset: 0x00031AB4
		public static string DumpPropValsMatrix(PropValue[][] propValsMatrix)
		{
			return TraceUtils.DumpArray(propValsMatrix);
		}
	}
}
