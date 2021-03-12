using System;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.PartnerToken;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000A9 RID: 169
	internal sealed class PartnerAccessToken
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x00013DAC File Offset: 0x00011FAC
		public PartnerAccessToken(PartnerIdentity partnerIdentity) : this(partnerIdentity.DelegatedOrganizationId.OrganizationalUnit, partnerIdentity.DelegatedOrganizationId.ConfigurationUnit, partnerIdentity.DelegatedPrincipal.DelegatedOrganization, partnerIdentity.DelegatedPrincipal.UserId)
		{
			if (partnerIdentity.DelegatedOrganizationId == OrganizationId.ForestWideOrgId)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[PartnerAccessToken::ctor] the first org id is not expected.");
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			if (string.IsNullOrEmpty(partnerIdentity.DelegatedPrincipal.DelegatedOrganization))
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug(0L, "[PartnerAccessToken::ctor] empty tenant name is not expected.");
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00013E49 File Offset: 0x00012049
		private PartnerAccessToken(ADObjectId organizationalUnit, ADObjectId configurationUnit, string tenantName, string partnerUser)
		{
			this.organizationalUnit = organizationalUnit;
			this.configurationUnit = configurationUnit;
			this.tenantName = tenantName;
			this.partnerUser = partnerUser;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00013E70 File Offset: 0x00012070
		public static PartnerAccessToken FromBytes(byte[] serializedTokenBytes)
		{
			int num = 0;
			if (serializedTokenBytes.Length < PartnerAccessToken.partnerAccessTokenCookie.Length + 1)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			for (int i = 0; i < PartnerAccessToken.partnerAccessTokenCookie.Length; i++)
			{
				if (serializedTokenBytes[num++] != PartnerAccessToken.partnerAccessTokenCookie[i])
				{
					throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
				}
			}
			if (serializedTokenBytes[num++] != 1)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			string text = SerializedSecurityAccessToken.DeserializeStringFromByteArray(serializedTokenBytes, ref num);
			string text2 = SerializedSecurityAccessToken.DeserializeStringFromByteArray(serializedTokenBytes, ref num);
			ADObjectId adobjectId = PartnerAccessToken.DeserializeADObjectIdFromByteArray(serializedTokenBytes, ref num);
			ADObjectId adobjectId2 = PartnerAccessToken.DeserializeADObjectIdFromByteArray(serializedTokenBytes, ref num);
			return new PartnerAccessToken(adobjectId, adobjectId2, text2, text);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00013F0E File Offset: 0x0001210E
		public OrganizationId OrganizationId
		{
			get
			{
				return this.OrganizationIdGetter();
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00013F16 File Offset: 0x00012116
		public string OrganizationName
		{
			get
			{
				return this.tenantName;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00013F1E File Offset: 0x0001211E
		public string PartnerUser
		{
			get
			{
				return this.partnerUser;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013F28 File Offset: 0x00012128
		public byte[] GetBytes()
		{
			byte[] array = new byte[this.GetByteCountToSerializeToken()];
			int num = 0;
			PartnerAccessToken.partnerAccessTokenCookie.CopyTo(array, num);
			num += PartnerAccessToken.partnerAccessTokenCookie.Length;
			array[num++] = 1;
			SerializedSecurityAccessToken.SerializeStringToByteArray(this.partnerUser, array, ref num);
			SerializedSecurityAccessToken.SerializeStringToByteArray(this.tenantName, array, ref num);
			PartnerAccessToken.SerializeADObjectIdToByteArray(this.organizationalUnit, array, ref num);
			PartnerAccessToken.SerializeADObjectIdToByteArray(this.configurationUnit, array, ref num);
			return array;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013F9C File Offset: 0x0001219C
		private static void SerializeADObjectIdToByteArray(ADObjectId adObjectId, byte[] byteArray, ref int byteIndex)
		{
			byte[] bytes = adObjectId.GetBytes(PartnerAccessToken.DefaultEncoding);
			int num = bytes.Length;
			byteIndex += ExBitConverter.Write(num, byteArray, byteIndex);
			Array.Copy(bytes, 0, byteArray, byteIndex, num);
			byteIndex += num;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013FD8 File Offset: 0x000121D8
		private static ADObjectId DeserializeADObjectIdFromByteArray(byte[] byteArray, ref int byteIndex)
		{
			int num = SerializedSecurityAccessToken.ReadInt32(byteArray, ref byteIndex);
			if (byteArray.Length < byteIndex + num)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			byte[] array = new byte[num];
			Array.Copy(byteArray, byteIndex, array, 0, num);
			byteIndex += num;
			ADObjectId result;
			try
			{
				result = new ADObjectId(array, PartnerAccessToken.DefaultEncoding);
			}
			catch (FormatException arg)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceWarning<FormatException>(0L, "[PartnerAccessToken::DeserializeADObjectIdFromByteArray] FormatException hit while creating a new ADObjectId. Exception: {0}", arg);
				throw FaultExceptionUtilities.CreateFault(new InvalidSerializedAccessTokenException(), FaultParty.Sender);
			}
			return result;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00014058 File Offset: 0x00012258
		private int GetByteCountToSerializeToken()
		{
			int num = 0;
			num += PartnerAccessToken.partnerAccessTokenCookie.Length;
			num++;
			num += 4;
			num += Encoding.UTF8.GetByteCount(this.partnerUser);
			num += 4;
			num += Encoding.UTF8.GetByteCount(this.tenantName);
			num += 4;
			num += this.organizationalUnit.GetByteCount(PartnerAccessToken.DefaultEncoding);
			num += 4;
			return num + this.configurationUnit.GetByteCount(PartnerAccessToken.DefaultEncoding);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000140D2 File Offset: 0x000122D2
		private OrganizationId OrganizationIdGetter()
		{
			return new OrganizationId(this.organizationalUnit, this.configurationUnit);
		}

		// Token: 0x04000628 RID: 1576
		private const byte PartnerAccessTokenVersion1 = 1;

		// Token: 0x04000629 RID: 1577
		private static readonly Encoding DefaultEncoding = Encoding.Unicode;

		// Token: 0x0400062A RID: 1578
		private static readonly byte[] partnerAccessTokenCookie = new byte[]
		{
			80,
			65,
			84
		};

		// Token: 0x0400062B RID: 1579
		private readonly ADObjectId organizationalUnit;

		// Token: 0x0400062C RID: 1580
		private readonly ADObjectId configurationUnit;

		// Token: 0x0400062D RID: 1581
		private readonly string tenantName;

		// Token: 0x0400062E RID: 1582
		private readonly string partnerUser;
	}
}
