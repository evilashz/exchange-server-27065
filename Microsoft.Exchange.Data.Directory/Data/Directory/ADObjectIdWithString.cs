using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000055 RID: 85
	public sealed class ADObjectIdWithString
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x00017EFE File Offset: 0x000160FE
		public ADObjectIdWithString(string stringValue, ADObjectId objectIdValue)
		{
			if (stringValue == null)
			{
				throw new ArgumentNullException("stringValue");
			}
			if (objectIdValue == null)
			{
				throw new ArgumentNullException("objectIdValue");
			}
			this.stringValue = stringValue;
			this.objectIdValue = objectIdValue;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00017F30 File Offset: 0x00016130
		internal ADObjectIdWithString(byte[] bytes)
		{
			int count = BitConverter.ToInt32(bytes, 0);
			this.stringValue = Encoding.Unicode.GetString(bytes, 4, count);
			this.objectIdValue = new ADObjectId(bytes, Encoding.Unicode, 4 + Encoding.Unicode.GetByteCount(this.stringValue));
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00017F81 File Offset: 0x00016181
		public string StringValue
		{
			get
			{
				return this.stringValue;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00017F89 File Offset: 0x00016189
		public ADObjectId ObjectIdValue
		{
			get
			{
				return this.objectIdValue;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00017F94 File Offset: 0x00016194
		public string ToDNStringSyntax(bool extendedDN)
		{
			string arg;
			if (extendedDN)
			{
				arg = this.objectIdValue.ToExtendedDN();
			}
			else
			{
				arg = this.objectIdValue.ToGuidOrDNString();
			}
			return string.Format("S:{0}:{1}:{2}", this.stringValue.Length, this.stringValue, arg);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00017FE4 File Offset: 0x000161E4
		public override bool Equals(object obj)
		{
			ADObjectIdWithString adobjectIdWithString = obj as ADObjectIdWithString;
			return adobjectIdWithString != null && this.stringValue.Equals(adobjectIdWithString.StringValue) && this.objectIdValue.Equals(adobjectIdWithString.ObjectIdValue);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00018023 File Offset: 0x00016223
		public bool Equals(ADObjectIdWithString other)
		{
			return this.stringValue.Equals(other.StringValue) && this.objectIdValue.Equals(other.ObjectIdValue);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001804B File Offset: 0x0001624B
		public override int GetHashCode()
		{
			return this.stringValue.GetHashCode() ^ this.objectIdValue.GetHashCode();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00018064 File Offset: 0x00016264
		public override string ToString()
		{
			return this.ToDNStringSyntax(false);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001806D File Offset: 0x0001626D
		internal static ADObjectIdWithString ParseDNStringSyntax(string value, OrganizationId orgId)
		{
			return ADObjectIdWithString.ParseDNStringSyntax(value, Guid.Empty, orgId);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001807C File Offset: 0x0001627C
		internal static ADObjectIdWithString ParseDNStringSyntax(string value, Guid partitionGuid, OrganizationId orgId)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string arg;
			string extendedDN;
			ADObjectIdWithString.ParseStringValue(value, out arg, out extendedDN);
			ADObjectId adobjectId = ADObjectId.ParseExtendedDN(extendedDN, partitionGuid, orgId);
			ExTraceGlobals.ADObjectTracer.TraceDebug<string>(0L, "ADObjectIdWithString.ParseDNStringSyntax - Initialized with string part {0}", arg);
			return new ADObjectIdWithString(arg, adobjectId);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000180C8 File Offset: 0x000162C8
		internal byte[] GetBytes()
		{
			int byteCount = this.GetByteCount();
			byte[] array = new byte[byteCount];
			int num = 0;
			int bytes = Encoding.Unicode.GetBytes(this.stringValue, 0, this.stringValue.Length, array, 4);
			num += ExBitConverter.Write(bytes, array, num);
			num += bytes;
			this.objectIdValue.GetBytes(Encoding.Unicode, array, num);
			return array;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00018128 File Offset: 0x00016328
		internal int GetByteCount()
		{
			int byteCount = Encoding.Unicode.GetByteCount(this.stringValue);
			int byteCount2 = this.objectIdValue.GetByteCount(Encoding.Unicode);
			return 4 + byteCount + byteCount2;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001815C File Offset: 0x0001635C
		private static void ParseStringValue(string dnString, out string stringPart, out string dnPart)
		{
			if (!ADObjectIdWithString.TryParseStringValue(dnString, out stringPart, out dnPart))
			{
				throw new FormatException(DirectoryStrings.InvalidDNStringFormat(dnString));
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001817C File Offset: 0x0001637C
		private static bool TryParseStringValue(string value, out string stringPart, out string dnPart)
		{
			stringPart = null;
			dnPart = null;
			if (!value.StartsWith("S:", StringComparison.Ordinal))
			{
				return false;
			}
			char c = ':';
			string[] array = value.Split(new char[]
			{
				c
			});
			if (array.Length < 4)
			{
				return false;
			}
			string text = array[1];
			int length;
			if (!int.TryParse(text, NumberStyles.None, null, out length))
			{
				return false;
			}
			int startIndex = 3 + text.Length;
			stringPart = value.Substring(startIndex, length);
			dnPart = array[array.Length - 1];
			return true;
		}

		// Token: 0x04000179 RID: 377
		private readonly string stringValue;

		// Token: 0x0400017A RID: 378
		private readonly ADObjectId objectIdValue;
	}
}
