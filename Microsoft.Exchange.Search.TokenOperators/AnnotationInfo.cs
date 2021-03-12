using System;
using System.Collections.Generic;
using Microsoft.Ceres.NlpBase.AnnotationStore;
using Microsoft.Ceres.NlpBase.AnnotationStore.Annotations;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000002 RID: 2
	internal sealed class AnnotationInfo
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal AnnotationInfo(IAnnotation annotation)
		{
			if (!AnnotationInfo.IsSupported(annotation))
			{
				throw new ArgumentException("annotation");
			}
			this.name = annotation.Name;
			this.start = annotation.Range.Start;
			this.end = annotation.Range.End;
			this.attributes = new List<KeyValuePair<string, string>>(annotation.Attributes);
			this.numericalAttributes = new List<KeyValuePair<string, double>>(annotation.NumericalAttributes);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000214C File Offset: 0x0000034C
		internal AnnotationInfo(string name, long start, int length)
		{
			if (!AnnotationInfo.IsSupportedName(name))
			{
				throw new ArgumentException("name");
			}
			this.name = name;
			this.start = start;
			this.end = start + (long)length;
			this.attributes = new List<KeyValuePair<string, string>>();
			this.numericalAttributes = new List<KeyValuePair<string, double>>();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021A0 File Offset: 0x000003A0
		internal AnnotationInfo(Range rawRange)
		{
			this.name = "token";
			this.start = rawRange.Start;
			this.end = rawRange.End;
			this.attributes = new List<KeyValuePair<string, string>>();
			this.numericalAttributes = new List<KeyValuePair<string, double>>();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021EE File Offset: 0x000003EE
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021F6 File Offset: 0x000003F6
		internal long Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021FE File Offset: 0x000003FE
		internal long End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002206 File Offset: 0x00000406
		internal IList<KeyValuePair<string, string>> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000220E File Offset: 0x0000040E
		internal IList<KeyValuePair<string, double>> NumericalAttributes
		{
			get
			{
				return this.numericalAttributes;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002216 File Offset: 0x00000416
		internal static bool IsSupported(IAnnotation annotation)
		{
			return AnnotationInfo.IsSupportedName(annotation.Name);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002223 File Offset: 0x00000423
		private static bool IsSupportedName(string name)
		{
			return name == "token" || name == "alttoken";
		}

		// Token: 0x04000001 RID: 1
		internal const string TokenName = "token";

		// Token: 0x04000002 RID: 2
		internal const string AltTokenName = "alttoken";

		// Token: 0x04000003 RID: 3
		private readonly string name;

		// Token: 0x04000004 RID: 4
		private readonly long start;

		// Token: 0x04000005 RID: 5
		private readonly long end;

		// Token: 0x04000006 RID: 6
		private readonly List<KeyValuePair<string, string>> attributes;

		// Token: 0x04000007 RID: 7
		private readonly List<KeyValuePair<string, double>> numericalAttributes;
	}
}
