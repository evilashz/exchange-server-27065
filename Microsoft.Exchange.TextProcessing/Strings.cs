using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200004B RID: 75
	internal static class Strings
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		static Strings()
		{
			Strings.stringIDs.Add(1734378792U, "OffsetsUnavailable");
			Strings.stringIDs.Add(2101368493U, "NullIMatch");
			Strings.stringIDs.Add(1322833356U, "CurrentNodeNotSingleNode");
			Strings.stringIDs.Add(1246524789U, "EmptyTermSet");
			Strings.stringIDs.Add(759104278U, "UnsupportedMatch");
			Strings.stringIDs.Add(901028467U, "NullFingerprint");
			Strings.stringIDs.Add(2821948495U, "TermExceedsMaximumLength");
			Strings.stringIDs.Add(608666739U, "InvalidTerm");
			Strings.stringIDs.Add(1141081490U, "IntermediateNodeHasMultipleChildren");
			Strings.stringIDs.Add(2382911857U, "InvalidData");
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		public static LocalizedString InvalidFingerprintSize(int size)
		{
			return new LocalizedString("InvalidFingerprintSize", Strings.ResourceManager, new object[]
			{
				size
			});
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000F82C File Offset: 0x0000DA2C
		public static LocalizedString InvalidShingle(string value)
		{
			return new LocalizedString("InvalidShingle", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000F854 File Offset: 0x0000DA54
		public static LocalizedString MismatchedFingerprintVersions(int version1, int version2)
		{
			return new LocalizedString("MismatchedFingerprintVersions", Strings.ResourceManager, new object[]
			{
				version1,
				version2
			});
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000F88A File Offset: 0x0000DA8A
		public static LocalizedString OffsetsUnavailable
		{
			get
			{
				return new LocalizedString("OffsetsUnavailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		public static LocalizedString MismatchedFingerprintSize(int size1, int size2)
		{
			return new LocalizedString("MismatchedFingerprintSize", Strings.ResourceManager, new object[]
			{
				size1,
				size2
			});
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000F8DC File Offset: 0x0000DADC
		public static LocalizedString InvalidFingerprintVersion(int version, int supportedVersion)
		{
			return new LocalizedString("InvalidFingerprintVersion", Strings.ResourceManager, new object[]
			{
				version,
				supportedVersion
			});
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000F912 File Offset: 0x0000DB12
		public static LocalizedString NullIMatch
		{
			get
			{
				return new LocalizedString("NullIMatch", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000F929 File Offset: 0x0000DB29
		public static LocalizedString CurrentNodeNotSingleNode
		{
			get
			{
				return new LocalizedString("CurrentNodeNotSingleNode", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000F940 File Offset: 0x0000DB40
		public static LocalizedString InvalidShingleCountForTemplate(ulong count)
		{
			return new LocalizedString("InvalidShingleCountForTemplate", Strings.ResourceManager, new object[]
			{
				count
			});
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000F96D File Offset: 0x0000DB6D
		public static LocalizedString EmptyTermSet
		{
			get
			{
				return new LocalizedString("EmptyTermSet", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000F984 File Offset: 0x0000DB84
		public static LocalizedString UnsupportedMatch
		{
			get
			{
				return new LocalizedString("UnsupportedMatch", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000F99C File Offset: 0x0000DB9C
		public static LocalizedString InvalidBoundaryType(string boundaryType)
		{
			return new LocalizedString("InvalidBoundaryType", Strings.ResourceManager, new object[]
			{
				boundaryType
			});
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		public static LocalizedString NullFingerprint
		{
			get
			{
				return new LocalizedString("NullFingerprint", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000F9DB File Offset: 0x0000DBDB
		public static LocalizedString TermExceedsMaximumLength
		{
			get
			{
				return new LocalizedString("TermExceedsMaximumLength", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000F9F2 File Offset: 0x0000DBF2
		public static LocalizedString InvalidTerm
		{
			get
			{
				return new LocalizedString("InvalidTerm", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000FA09 File Offset: 0x0000DC09
		public static LocalizedString IntermediateNodeHasMultipleChildren
		{
			get
			{
				return new LocalizedString("IntermediateNodeHasMultipleChildren", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000FA20 File Offset: 0x0000DC20
		public static LocalizedString InvalidCoefficient(double value)
		{
			return new LocalizedString("InvalidCoefficient", Strings.ResourceManager, new object[]
			{
				value
			});
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000FA4D File Offset: 0x0000DC4D
		public static LocalizedString InvalidData
		{
			get
			{
				return new LocalizedString("InvalidData", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000FA64 File Offset: 0x0000DC64
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400018F RID: 399
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(10);

		// Token: 0x04000190 RID: 400
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.TextProcessing.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200004C RID: 76
		public enum IDs : uint
		{
			// Token: 0x04000192 RID: 402
			OffsetsUnavailable = 1734378792U,
			// Token: 0x04000193 RID: 403
			NullIMatch = 2101368493U,
			// Token: 0x04000194 RID: 404
			CurrentNodeNotSingleNode = 1322833356U,
			// Token: 0x04000195 RID: 405
			EmptyTermSet = 1246524789U,
			// Token: 0x04000196 RID: 406
			UnsupportedMatch = 759104278U,
			// Token: 0x04000197 RID: 407
			NullFingerprint = 901028467U,
			// Token: 0x04000198 RID: 408
			TermExceedsMaximumLength = 2821948495U,
			// Token: 0x04000199 RID: 409
			InvalidTerm = 608666739U,
			// Token: 0x0400019A RID: 410
			IntermediateNodeHasMultipleChildren = 1141081490U,
			// Token: 0x0400019B RID: 411
			InvalidData = 2382911857U
		}

		// Token: 0x0200004D RID: 77
		private enum ParamIDs
		{
			// Token: 0x0400019D RID: 413
			InvalidFingerprintSize,
			// Token: 0x0400019E RID: 414
			InvalidShingle,
			// Token: 0x0400019F RID: 415
			MismatchedFingerprintVersions,
			// Token: 0x040001A0 RID: 416
			MismatchedFingerprintSize,
			// Token: 0x040001A1 RID: 417
			InvalidFingerprintVersion,
			// Token: 0x040001A2 RID: 418
			InvalidShingleCountForTemplate,
			// Token: 0x040001A3 RID: 419
			InvalidBoundaryType,
			// Token: 0x040001A4 RID: 420
			InvalidCoefficient
		}
	}
}
