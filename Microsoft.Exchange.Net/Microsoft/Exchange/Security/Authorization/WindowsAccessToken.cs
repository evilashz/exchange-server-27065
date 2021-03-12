using System;
using System.IO;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000651 RID: 1617
	public sealed class WindowsAccessToken : SecurityAccessToken
	{
		// Token: 0x06001D5A RID: 7514 RVA: 0x00035F40 File Offset: 0x00034140
		internal WindowsAccessToken(string logonName, string authenticationType, ClientSecurityContext clientSecurityContext, CommonAccessToken commonAccessToken)
		{
			this.LogonName = logonName;
			this.AuthenticationType = authenticationType;
			this.commonAccessToken = commonAccessToken;
			clientSecurityContext.SetSecurityAccessToken(this);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00035F65 File Offset: 0x00034165
		internal WindowsAccessToken(CommonAccessToken commonAccessToken)
		{
			this.commonAccessToken = commonAccessToken;
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x00035F74 File Offset: 0x00034174
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x00035F7C File Offset: 0x0003417C
		public string LogonName { get; private set; }

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x00035F85 File Offset: 0x00034185
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x00035F8D File Offset: 0x0003418D
		public string AuthenticationType { get; private set; }

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x00035F96 File Offset: 0x00034196
		internal CommonAccessToken CommonAccessToken
		{
			get
			{
				return this.commonAccessToken;
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00035FA0 File Offset: 0x000341A0
		internal byte[] GetSerializedToken()
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					this.SerializeToken(binaryWriter);
					binaryWriter.Flush();
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00036004 File Offset: 0x00034204
		internal void DeserializeFromToken(byte[] token)
		{
			using (MemoryStream memoryStream = new MemoryStream(token))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					this.commonAccessToken.ReadAndValidateFieldType(binaryReader, 'A', AuthorizationStrings.AuthenticationTypeIsMissing);
					this.AuthenticationType = this.commonAccessToken.BinaryRead<string>(new Func<string>(binaryReader.ReadString), AuthorizationStrings.AuthenticationTypeIsMissing);
					this.commonAccessToken.ReadAndValidateFieldType(binaryReader, 'L', AuthorizationStrings.LogonNameIsMissing);
					this.LogonName = this.commonAccessToken.BinaryRead<string>(new Func<string>(binaryReader.ReadString), AuthorizationStrings.LogonNameIsMissing);
					this.commonAccessToken.ReadAndValidateFieldType(binaryReader, 'U', AuthorizationStrings.MissingUserSid);
					base.UserSid = this.commonAccessToken.BinaryRead<string>(new Func<string>(binaryReader.ReadString), AuthorizationStrings.MissingUserSid);
					if (string.IsNullOrEmpty(base.UserSid))
					{
						throw new CommonAccessTokenException((int)this.commonAccessToken.Version, AuthorizationStrings.MissingUserSid);
					}
					this.ReadGroupsAndExtensionData(binaryReader);
				}
			}
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00036120 File Offset: 0x00034320
		private static void WriteGroups(BinaryWriter writer, SidStringAndAttributes[] groups, char fieldType)
		{
			if (groups != null)
			{
				writer.Write(fieldType);
				writer.Write(groups.Length);
				for (int i = 0; i < groups.Length; i++)
				{
					WindowsAccessToken.WriteSid(writer, groups[i].SecurityIdentifier, groups[i].Attributes);
				}
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x00036164 File Offset: 0x00034364
		private static void WriteSid(BinaryWriter writer, string sid, uint attributes)
		{
			writer.Write(attributes);
			writer.Write(sid);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x00036174 File Offset: 0x00034374
		private void SerializeToken(BinaryWriter writer)
		{
			writer.Write('A');
			writer.Write(this.AuthenticationType);
			writer.Write('L');
			writer.Write(this.LogonName);
			writer.Write('U');
			writer.Write(base.UserSid);
			WindowsAccessToken.WriteGroups(writer, base.GroupSids, 'G');
			WindowsAccessToken.WriteGroups(writer, base.RestrictedGroupSids, 'R');
			this.commonAccessToken.WriteExtensionData(writer);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000361E8 File Offset: 0x000343E8
		private void ReadGroupsAndExtensionData(BinaryReader reader)
		{
			int num = 0;
			while (reader.PeekChar() >= 0)
			{
				char c = reader.ReadChar();
				if (c == 'G')
				{
					int num2 = this.commonAccessToken.BinaryRead<int>(new Func<int>(reader.ReadInt32), AuthorizationStrings.InvalidGroupLength);
					num += num2;
					if (num > 3000)
					{
						throw new CommonAccessTokenException((int)this.commonAccessToken.Version, AuthorizationStrings.TooManySidNodes(this.LogonName, 3000));
					}
					base.GroupSids = new SidStringAndAttributes[num2];
					for (int i = 0; i < num2; i++)
					{
						uint num3 = this.commonAccessToken.BinaryRead<uint>(new Func<uint>(reader.ReadUInt32), AuthorizationStrings.InvalidGroupAttributes);
						if (num3 == 0U)
						{
							throw new CommonAccessTokenException((int)this.commonAccessToken.Version, AuthorizationStrings.InvalidGroupAttributesValue);
						}
						string identifier = this.commonAccessToken.BinaryRead<string>(new Func<string>(reader.ReadString), AuthorizationStrings.InvalidGroupSidValue);
						base.GroupSids[i] = new SidStringAndAttributes(identifier, num3);
					}
				}
				else if (c == 'R')
				{
					int num2 = this.commonAccessToken.BinaryRead<int>(new Func<int>(reader.ReadInt32), AuthorizationStrings.InvalidRestrictedGroupLength);
					num += num2;
					if (num > 3000)
					{
						throw new CommonAccessTokenException((int)this.commonAccessToken.Version, AuthorizationStrings.TooManySidNodes(this.LogonName, 3000));
					}
					base.RestrictedGroupSids = new SidStringAndAttributes[num2];
					for (int j = 0; j < num2; j++)
					{
						uint num4 = this.commonAccessToken.BinaryRead<uint>(new Func<uint>(reader.ReadUInt32), AuthorizationStrings.InvalidRestrictedGroupAttributes);
						if (num4 == 0U)
						{
							throw new CommonAccessTokenException((int)this.commonAccessToken.Version, AuthorizationStrings.InvalidRestrictedGroupAttributesValue);
						}
						string identifier2 = this.commonAccessToken.BinaryRead<string>(new Func<string>(reader.ReadString), AuthorizationStrings.InvalidRestrictedGroupSidValue);
						base.RestrictedGroupSids[j] = new SidStringAndAttributes(identifier2, num4);
					}
				}
				else
				{
					if (c != 'E')
					{
						throw new CommonAccessTokenException((int)this.commonAccessToken.Version, AuthorizationStrings.InvalidFieldType);
					}
					this.commonAccessToken.ReadExtensionData(reader);
				}
			}
		}

		// Token: 0x04001DA6 RID: 7590
		private const int MaximumSidsPerContext = 3000;

		// Token: 0x04001DA7 RID: 7591
		private CommonAccessToken commonAccessToken;
	}
}
