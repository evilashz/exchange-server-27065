using System;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OneOffEntryId : AddressEntryId
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002107 File Offset: 0x00000307
		private OneOffEntryId(uint flags, OneOffDataType type, string displayName, string addressType, string emailAddress, Encoding string8Encoding) : base(null)
		{
			this.flags = flags;
			this.type = type;
			this.displayName = displayName;
			this.addressType = addressType;
			this.emailAddress = emailAddress;
			this.string8Encoding = string8Encoding;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002140 File Offset: 0x00000340
		public static bool TryParse(Reader reader, Encoding string8Encoding, out OneOffEntryId oneoffEntryId, uint sizeEntry, ref string error)
		{
			oneoffEntryId = null;
			long position = reader.Position;
			if (!OneOffEntryId.InternalTryParse(reader, string8Encoding, out oneoffEntryId, ref error))
			{
				return false;
			}
			long num = reader.Position - position;
			if ((ulong)sizeEntry != (ulong)num)
			{
				error = string.Format("TryParse: sizeEntry ({0}) != actualSize ({1})", sizeEntry, num);
				return false;
			}
			return true;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002190 File Offset: 0x00000390
		public new static OneOffEntryId Parse(Reader reader, Encoding string8Encoding, uint sizeEntry)
		{
			string arg = null;
			OneOffEntryId result;
			if (!OneOffEntryId.TryParse(reader, string8Encoding, out result, sizeEntry, ref arg))
			{
				throw new BufferParseException(string.Format("Cannot parse this entryId as a OneOffEntryId. sizeEntry = {0}. Encoding {1}. Error: {2}.", sizeEntry, string8Encoding, arg));
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C6 File Offset: 0x000003C6
		public byte[] Serialize()
		{
			return AddressEntryId.ToBytes(new BufferWriter.SerializeDelegate(this.Serialize));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021DC File Offset: 0x000003DC
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32(this.flags);
			writer.WriteBytes(OneOffEntryId.MapiProviderGuidBytes);
			writer.WriteUInt32((uint)this.type);
			if ((this.type & (OneOffDataType)2147483648U) != (OneOffDataType)0U)
			{
				writer.WriteUnicodeString(this.displayName, StringFlags.IncludeNull);
				writer.WriteUnicodeString(this.addressType, StringFlags.IncludeNull);
				writer.WriteUnicodeString(this.emailAddress, StringFlags.IncludeNull);
				return;
			}
			writer.WriteString8(this.displayName, this.string8Encoding, StringFlags.IncludeNull);
			writer.WriteString8(this.addressType, this.string8Encoding, StringFlags.IncludeNull);
			writer.WriteString8(this.emailAddress, this.string8Encoding, StringFlags.IncludeNull);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000227B File Offset: 0x0000047B
		public override void SetUnicode()
		{
			this.type |= (OneOffDataType)2147483648U;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000228F File Offset: 0x0000048F
		public override void SetString8(Encoding string8Encoding)
		{
			this.string8Encoding = string8Encoding;
			this.type &= ~OneOffDataType.Unicode;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022AC File Offset: 0x000004AC
		private static bool InternalTryParse(Reader reader, Encoding string8Encoding, out OneOffEntryId oneoffEntryId, ref string error)
		{
			oneoffEntryId = null;
			try
			{
				uint num = reader.ReadUInt32();
				byte[] array = reader.ReadBytes(16U);
				if (!ArrayComparer<byte>.Comparer.Equals(OneOffEntryId.MapiProviderGuidBytes, array))
				{
					error = string.Format("AddressEntryId: Not mapi provider guid {0}", new Guid(array));
					return false;
				}
				OneOffDataType oneOffDataType = (OneOffDataType)reader.ReadUInt32();
				string text;
				string text2;
				string text3;
				if ((oneOffDataType & (OneOffDataType)2147483648U) != (OneOffDataType)0U)
				{
					text = reader.ReadUnicodeString(StringFlags.IncludeNull);
					text2 = reader.ReadUnicodeString(StringFlags.IncludeNull);
					text3 = reader.ReadUnicodeString(StringFlags.IncludeNull);
				}
				else
				{
					text = reader.ReadString8(string8Encoding, StringFlags.IncludeNull);
					text2 = reader.ReadString8(string8Encoding, StringFlags.IncludeNull);
					text3 = reader.ReadString8(string8Encoding, StringFlags.IncludeNull);
				}
				oneoffEntryId = new OneOffEntryId(num, oneOffDataType, text, text2, text3, string8Encoding);
			}
			catch (BufferParseException ex)
			{
				ExTraceGlobals.FailedRopTracer.TraceWarning<BufferParseException>(0L, "Attempting to parse a OneOffEntryId failed with exception: {0}", ex);
				error = ex.ToString();
				return false;
			}
			return true;
		}

		// Token: 0x04000004 RID: 4
		public const int HeaderLength = 24;

		// Token: 0x04000005 RID: 5
		private static readonly byte[] MapiProviderGuidBytes = new Guid("{a41f2b81-a3be-1910-9d6e-00dd010f5402}").ToByteArray();

		// Token: 0x04000006 RID: 6
		private readonly uint flags;

		// Token: 0x04000007 RID: 7
		private readonly string displayName;

		// Token: 0x04000008 RID: 8
		private readonly string addressType;

		// Token: 0x04000009 RID: 9
		private readonly string emailAddress;

		// Token: 0x0400000A RID: 10
		private OneOffDataType type;

		// Token: 0x0400000B RID: 11
		private Encoding string8Encoding;
	}
}
