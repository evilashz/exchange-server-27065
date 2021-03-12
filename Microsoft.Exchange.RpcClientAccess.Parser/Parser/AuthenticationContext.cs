using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000005 RID: 5
	internal class AuthenticationContext
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002350 File Offset: 0x00000550
		public AuthenticationContext(string connectAs, SecurityIdentifier userSid, int primaryGroupIndex, SidBinaryAndAttributes[] regularGroups, SidBinaryAndAttributes[] restrictedGroups)
		{
			if (userSid == null)
			{
				throw new ArgumentNullException("userSid");
			}
			this.connectAs = (connectAs ?? string.Empty);
			this.userSid = userSid;
			this.primaryGroupIndex = primaryGroupIndex;
			this.regularGroups = (regularGroups ?? Array<SidBinaryAndAttributes>.Empty);
			this.restrictedGroups = (restrictedGroups ?? Array<SidBinaryAndAttributes>.Empty);
			if (primaryGroupIndex < -1 || primaryGroupIndex >= this.regularGroups.Length)
			{
				throw new ArgumentException("primaryGroupIndex");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023D1 File Offset: 0x000005D1
		public string ConnectAs
		{
			get
			{
				return this.connectAs;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000023D9 File Offset: 0x000005D9
		public SecurityIdentifier UserSid
		{
			get
			{
				return this.userSid;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000023E1 File Offset: 0x000005E1
		public int PrimaryGroupIndex
		{
			get
			{
				return this.primaryGroupIndex;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023E9 File Offset: 0x000005E9
		public SidBinaryAndAttributes[] RegularGroups
		{
			get
			{
				return this.regularGroups;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000023F1 File Offset: 0x000005F1
		public SidBinaryAndAttributes[] RestrictedGroups
		{
			get
			{
				return this.restrictedGroups;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023FC File Offset: 0x000005FC
		public static AuthenticationContext Parse(Reader reader)
		{
			long position = reader.Position;
			uint num = (uint)reader.ReadByte();
			if (num != 0U)
			{
				throw new BufferParseException("Unsupported AuthenticationContext flags");
			}
			uint num2 = reader.ReadUInt32();
			if (num2 == 0U)
			{
				throw new BufferParseException("Empty AuthenticationContext buffer.");
			}
			if ((ulong)num2 > (ulong)(reader.Length - position) || num2 > 65535U)
			{
				throw new BufferParseException("Invalid size encoded in AuthenticationContext buffer.");
			}
			string text = reader.ReadAsciiString(StringFlags.IncludeNull | StringFlags.Sized16 | StringFlags.FailOnError);
			SecurityIdentifier securityIdentifier = reader.ReadSecurityIdentifier();
			SidBinaryAndAttributes[] array = AuthenticationContext.ParseGroups(reader);
			short num3 = (short)reader.ReadUInt16();
			if (num3 < -1 || (int)num3 >= array.Length)
			{
				throw new BufferParseException("Invalid primary group index encoded in AuthenticationContext buffer.");
			}
			SidBinaryAndAttributes[] array2 = AuthenticationContext.ParseGroups(reader);
			long position2 = reader.Position;
			if (position2 - position != (long)((ulong)num2))
			{
				throw new BufferParseException("AuthenticationContext buffer had unexpected size.");
			}
			return new AuthenticationContext(text, securityIdentifier, (int)num3, array, array2);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024C4 File Offset: 0x000006C4
		public void Serialize(Writer writer)
		{
			long position = writer.Position;
			writer.WriteByte(0);
			long position2 = writer.Position;
			writer.WriteUInt32(0U);
			writer.WriteAsciiString(this.ConnectAs, StringFlags.IncludeNull | StringFlags.Sized16);
			writer.WriteSecurityIdentifier(this.UserSid);
			AuthenticationContext.SerializeGroups(writer, this.RegularGroups);
			writer.WriteUInt16((ushort)this.PrimaryGroupIndex);
			AuthenticationContext.SerializeGroups(writer, this.RestrictedGroups);
			long position3 = writer.Position;
			long num = position3 - position;
			writer.Position = position2;
			writer.WriteUInt32((uint)num);
			writer.Position = position3;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000254C File Offset: 0x0000074C
		public override string ToString()
		{
			return string.Format("AuthenticationContext: [connectAs={0}],[userSid={1}]", this.ConnectAs, this.UserSid);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002564 File Offset: 0x00000764
		private static SidBinaryAndAttributes[] ParseGroups(Reader reader)
		{
			ushort num = reader.ReadUInt16();
			SidBinaryAndAttributes[] array = new SidBinaryAndAttributes[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				SecurityIdentifier identifier = reader.ReadSecurityIdentifier();
				GroupAttributes attribute = (GroupAttributes)reader.ReadUInt32();
				array[i] = new SidBinaryAndAttributes(identifier, (uint)attribute);
			}
			return array;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025A8 File Offset: 0x000007A8
		private static void SerializeGroups(Writer writer, SidBinaryAndAttributes[] group)
		{
			ushort num = (ushort)((group == null) ? 0 : group.Length);
			writer.WriteUInt16(num);
			if (num > 0)
			{
				foreach (SidBinaryAndAttributes sidBinaryAndAttributes in group)
				{
					writer.WriteSecurityIdentifier(sidBinaryAndAttributes.SecurityIdentifier);
					writer.WriteUInt32(sidBinaryAndAttributes.Attributes);
				}
			}
		}

		// Token: 0x04000008 RID: 8
		private const byte Flags = 0;

		// Token: 0x04000009 RID: 9
		internal const int MaxSize = 65535;

		// Token: 0x0400000A RID: 10
		private readonly string connectAs;

		// Token: 0x0400000B RID: 11
		private readonly SecurityIdentifier userSid;

		// Token: 0x0400000C RID: 12
		private readonly int primaryGroupIndex;

		// Token: 0x0400000D RID: 13
		private readonly SidBinaryAndAttributes[] regularGroups;

		// Token: 0x0400000E RID: 14
		private readonly SidBinaryAndAttributes[] restrictedGroups;
	}
}
