using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200033B RID: 827
	internal sealed class GetUserConfiguration : UserConfigurationCommandBase<GetUserConfigurationRequest, ServiceUserConfiguration>
	{
		// Token: 0x06001722 RID: 5922 RVA: 0x0007B480 File Offset: 0x00079680
		public GetUserConfiguration(CallContext callContext, GetUserConfigurationRequest request) : base(callContext, request)
		{
			this.userConfigurationName = base.Request.UserConfigurationName;
			this.userConfigurationProperties = base.Request.UserConfigurationProperties;
			ServiceCommandBase.ThrowIfNull(this.userConfigurationName, "userConfigurationName", "GetUserConfiguration:Execute");
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0007B4CC File Offset: 0x000796CC
		internal override IExchangeWebMethodResponse GetResponse()
		{
			GetUserConfigurationResponse getUserConfigurationResponse = new GetUserConfigurationResponse();
			getUserConfigurationResponse.ProcessServiceResult(base.Result);
			return getUserConfigurationResponse;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0007B4EC File Offset: 0x000796EC
		internal override ServiceResult<ServiceUserConfiguration> Execute()
		{
			UserConfigurationCommandBase<GetUserConfigurationRequest, ServiceUserConfiguration>.UserConfigurationName userConfigurationName = base.GetUserConfigurationName(this.userConfigurationName);
			ServiceUserConfiguration value;
			using (UserConfiguration userConfiguration = UserConfigurationCommandBase<GetUserConfigurationRequest, ServiceUserConfiguration>.Get(userConfigurationName))
			{
				value = this.PrepareServiceUserConfiguration(userConfiguration, userConfigurationName.MailboxSession);
			}
			return new ServiceResult<ServiceUserConfiguration>(value);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0007B540 File Offset: 0x00079740
		private ServiceUserConfiguration PrepareServiceUserConfiguration(UserConfiguration userConfiguration, MailboxSession mailboxSession)
		{
			ServiceUserConfiguration serviceUserConfiguration = new ServiceUserConfiguration();
			serviceUserConfiguration.UserConfigurationName = this.userConfigurationName;
			if (this.IsPropertyRequested(UserConfigurationProperties.Id))
			{
				serviceUserConfiguration.ItemId = this.GetItemId(userConfiguration, mailboxSession);
			}
			if (this.IsPropertyRequested(UserConfigurationProperties.Dictionary) && UserConfigurationCommandBase<GetUserConfigurationRequest, ServiceUserConfiguration>.PropertyExists(UserConfigurationTypes.Dictionary, userConfiguration.DataTypes))
			{
				serviceUserConfiguration.Dictionary = this.GetDictionaryEntries(userConfiguration);
			}
			if (this.IsPropertyRequested(UserConfigurationProperties.BinaryData) && UserConfigurationCommandBase<GetUserConfigurationRequest, ServiceUserConfiguration>.PropertyExists(UserConfigurationTypes.Stream, userConfiguration.DataTypes))
			{
				serviceUserConfiguration.BinaryData = this.GetBinaryData(userConfiguration);
			}
			if (this.IsPropertyRequested(UserConfigurationProperties.XmlData) && UserConfigurationCommandBase<GetUserConfigurationRequest, ServiceUserConfiguration>.PropertyExists(UserConfigurationTypes.XML, userConfiguration.DataTypes))
			{
				serviceUserConfiguration.XmlData = this.GetXmlData(userConfiguration);
			}
			return serviceUserConfiguration;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x0007B5E3 File Offset: 0x000797E3
		private bool IsPropertyRequested(UserConfigurationProperties userConfigurationProperties)
		{
			return (userConfigurationProperties & this.userConfigurationProperties) == userConfigurationProperties;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x0007B5F0 File Offset: 0x000797F0
		private ItemId GetItemId(UserConfiguration userConfiguration, MailboxSession mailboxSession)
		{
			ServiceXml.CreateElement(new SafeXmlDocument(), "TemporaryContainerName", ServiceXml.DefaultNamespaceUri);
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(userConfiguration.VersionedId, new MailboxId(mailboxSession), null);
			return new ItemId
			{
				Id = concatenatedId.Id,
				ChangeKey = concatenatedId.ChangeKey
			};
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0007B648 File Offset: 0x00079848
		private UserConfigurationDictionaryEntry[] GetDictionaryEntries(UserConfiguration userConfiguration)
		{
			UserConfigurationDictionaryEntry[] array = null;
			ConfigurationDictionary dictionary = UserConfigurationCommandBase<GetUserConfigurationRequest, ServiceUserConfiguration>.GetDictionary(userConfiguration);
			if (dictionary.Count > 0)
			{
				array = new UserConfigurationDictionaryEntry[dictionary.Count];
				int num = 0;
				foreach (object obj in dictionary)
				{
					DictionaryEntry storeDictionaryEntry = (DictionaryEntry)obj;
					array[num] = this.CreateUserConfigurationDictionaryEntry(storeDictionaryEntry);
					num++;
				}
			}
			return array;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0007B6CC File Offset: 0x000798CC
		private UserConfigurationDictionaryEntry CreateUserConfigurationDictionaryEntry(DictionaryEntry storeDictionaryEntry)
		{
			UserConfigurationDictionaryEntry userConfigurationDictionaryEntry = new UserConfigurationDictionaryEntry();
			userConfigurationDictionaryEntry.DictionaryKey = this.ConstructDictionaryObject(storeDictionaryEntry.Key);
			if (storeDictionaryEntry.Value != null)
			{
				userConfigurationDictionaryEntry.DictionaryValue = this.ConstructDictionaryObject(storeDictionaryEntry.Value);
			}
			return userConfigurationDictionaryEntry;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x0007B710 File Offset: 0x00079910
		private UserConfigurationDictionaryObject ConstructDictionaryObject(object storeDictionaryObject)
		{
			UserConfigurationDictionaryObject userConfigurationDictionaryObject = new UserConfigurationDictionaryObject();
			string[] array = storeDictionaryObject as string[];
			if (array != null)
			{
				userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.StringArray;
				userConfigurationDictionaryObject.Value = array;
			}
			else
			{
				userConfigurationDictionaryObject.Value = new string[1];
				byte[] array2 = storeDictionaryObject as byte[];
				if (array2 != null)
				{
					userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.ByteArray;
					userConfigurationDictionaryObject.Value[0] = Base64StringConverter.ToString(array2);
				}
				else if (storeDictionaryObject is ExDateTime)
				{
					userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.DateTime;
					userConfigurationDictionaryObject.Value[0] = ExDateTimeConverter.ToUtcXsdDateTime((ExDateTime)storeDictionaryObject);
				}
				else
				{
					TypeCode typeCode = Type.GetTypeCode(storeDictionaryObject.GetType());
					switch (typeCode)
					{
					case TypeCode.Boolean:
						userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.Boolean;
						userConfigurationDictionaryObject.Value[0] = BooleanConverter.ToString((bool)storeDictionaryObject);
						break;
					case TypeCode.Char:
					case TypeCode.SByte:
					case TypeCode.Int16:
					case TypeCode.UInt16:
						break;
					case TypeCode.Byte:
						userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.Byte;
						userConfigurationDictionaryObject.Value[0] = ByteConverter.ToString((byte)storeDictionaryObject);
						break;
					case TypeCode.Int32:
						userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.Integer32;
						userConfigurationDictionaryObject.Value[0] = IntConverter.ToString((int)storeDictionaryObject);
						break;
					case TypeCode.UInt32:
						userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.UnsignedInteger32;
						userConfigurationDictionaryObject.Value[0] = UIntConverter.ToString((uint)storeDictionaryObject);
						break;
					case TypeCode.Int64:
						userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.Integer64;
						userConfigurationDictionaryObject.Value[0] = LongConverter.ToString((long)storeDictionaryObject);
						break;
					case TypeCode.UInt64:
						userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.UnsignedInteger64;
						userConfigurationDictionaryObject.Value[0] = ULongConverter.ToString((ulong)storeDictionaryObject);
						break;
					default:
						if (typeCode == TypeCode.String)
						{
							userConfigurationDictionaryObject.Type = UserConfigurationDictionaryObjectType.String;
							userConfigurationDictionaryObject.Value[0] = (string)storeDictionaryObject;
						}
						break;
					}
				}
			}
			return userConfigurationDictionaryObject;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0007B8A4 File Offset: 0x00079AA4
		private string GetBinaryData(UserConfiguration userConfiguration)
		{
			string streamPropertyAsBase64String;
			using (Stream stream = UserConfigurationCommandBase<GetUserConfigurationRequest, ServiceUserConfiguration>.GetStream(userConfiguration))
			{
				streamPropertyAsBase64String = this.GetStreamPropertyAsBase64String(stream);
			}
			return streamPropertyAsBase64String;
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0007B8E0 File Offset: 0x00079AE0
		private string GetStreamPropertyAsBase64String(Stream stream)
		{
			string result = null;
			if (stream.Length > 0L)
			{
				byte[] array = new byte[stream.Length];
				int num = 0;
				using (BinaryReader binaryReader = new BinaryReader(stream))
				{
					int num3;
					do
					{
						int num2 = Math.Min(4096, array.Length - num);
						num3 = ((num2 > 0) ? binaryReader.Read(array, num, num2) : 0);
						num += num3;
					}
					while (num3 > 0);
				}
				result = Convert.ToBase64String(array, 0, num);
			}
			return result;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0007B968 File Offset: 0x00079B68
		private string GetXmlData(UserConfiguration userConfiguration)
		{
			string streamPropertyAsBase64String;
			using (Stream xmlStream = UserConfigurationCommandBase<GetUserConfigurationRequest, ServiceUserConfiguration>.GetXmlStream(userConfiguration))
			{
				streamPropertyAsBase64String = this.GetStreamPropertyAsBase64String(xmlStream);
			}
			return streamPropertyAsBase64String;
		}

		// Token: 0x04000FB4 RID: 4020
		private UserConfigurationNameType userConfigurationName;

		// Token: 0x04000FB5 RID: 4021
		private UserConfigurationProperties userConfigurationProperties;
	}
}
