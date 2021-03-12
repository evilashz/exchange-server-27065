using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002C8 RID: 712
	internal abstract class UserConfigurationCommandBase<RequestType, SingleItemType> : SingleStepServiceCommand<RequestType, SingleItemType> where RequestType : BaseRequest
	{
		// Token: 0x060013BC RID: 5052 RVA: 0x00062D3A File Offset: 0x00060F3A
		internal UserConfigurationCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00062D44 File Offset: 0x00060F44
		protected static UserConfiguration Get(UserConfigurationCommandBase<RequestType, SingleItemType>.UserConfigurationName userConfigurationName)
		{
			return userConfigurationName.MailboxSession.UserConfigurationManager.GetFolderConfiguration(userConfigurationName.Name, UserConfigurationTypes.Stream | UserConfigurationTypes.XML | UserConfigurationTypes.Dictionary, userConfigurationName.FolderId);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00062D63 File Offset: 0x00060F63
		protected static void ValidatePropertiesForUpdate(ServiceUserConfiguration serviceUserConfiguration)
		{
			if (serviceUserConfiguration.ItemId != null)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)2503843052U);
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00062D7D File Offset: 0x00060F7D
		protected static void SetProperties(ServiceUserConfiguration serviceUserConfiguration, UserConfiguration userConfiguration)
		{
			UserConfigurationCommandBase<RequestType, SingleItemType>.SetDictionary(serviceUserConfiguration, userConfiguration);
			UserConfigurationCommandBase<RequestType, SingleItemType>.SetXmlStream(serviceUserConfiguration, userConfiguration);
			UserConfigurationCommandBase<RequestType, SingleItemType>.SetStream(serviceUserConfiguration, userConfiguration);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00062D94 File Offset: 0x00060F94
		protected static void UpdateProperties(ServiceUserConfiguration serviceUserConfiguration, UserConfiguration userConfiguration)
		{
			UserConfigurationCommandBase<RequestType, SingleItemType>.UpdateDictionary(serviceUserConfiguration, userConfiguration);
			UserConfigurationCommandBase<RequestType, SingleItemType>.SetXmlStream(serviceUserConfiguration, userConfiguration);
			UserConfigurationCommandBase<RequestType, SingleItemType>.SetStream(serviceUserConfiguration, userConfiguration);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00062DAB File Offset: 0x00060FAB
		protected static ConfigurationDictionary GetDictionary(UserConfiguration userConfiguration)
		{
			if (!UserConfigurationCommandBase<RequestType, SingleItemType>.PropertyExists(UserConfigurationTypes.Dictionary, userConfiguration.DataTypes))
			{
				throw new ObjectSavePropertyErrorException((CoreResources.IDs)2214456911U, null);
			}
			return (ConfigurationDictionary)userConfiguration.GetDictionary();
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00062DD7 File Offset: 0x00060FD7
		protected static Stream GetXmlStream(UserConfiguration userConfiguration)
		{
			if (!UserConfigurationCommandBase<RequestType, SingleItemType>.PropertyExists(UserConfigurationTypes.XML, userConfiguration.DataTypes))
			{
				throw new ObjectSavePropertyErrorException((CoreResources.IDs)2419720676U, null);
			}
			return userConfiguration.GetXmlStream();
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00062DFE File Offset: 0x00060FFE
		protected static Stream GetStream(UserConfiguration userConfiguration)
		{
			if (!UserConfigurationCommandBase<RequestType, SingleItemType>.PropertyExists(UserConfigurationTypes.Stream, userConfiguration.DataTypes))
			{
				throw new ObjectSavePropertyErrorException(CoreResources.IDs.ErrorUserConfigurationBinaryDataNotExist, null);
			}
			return userConfiguration.GetStream();
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00062E25 File Offset: 0x00061025
		protected static bool PropertyExists(UserConfigurationTypes userConfigurationPropertyToCheck, UserConfigurationTypes userConfigurationProperties)
		{
			return (userConfigurationPropertyToCheck & userConfigurationProperties) != (UserConfigurationTypes)0;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00062E30 File Offset: 0x00061030
		protected UserConfigurationCommandBase<RequestType, SingleItemType>.UserConfigurationName GetUserConfigurationName(UserConfigurationNameType userConfigurationName)
		{
			IdAndSession idAndSession = base.IdConverter.ConvertTargetFolderIdToIdAndContentSession(userConfigurationName.BaseFolderId, true);
			if (idAndSession.Session is PublicFolderSession)
			{
				throw new InvalidValueForPropertyException((CoreResources.IDs)3014743008U, null);
			}
			return new UserConfigurationCommandBase<RequestType, SingleItemType>.UserConfigurationName(userConfigurationName.Name, idAndSession.Id, (MailboxSession)idAndSession.Session);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00062E8C File Offset: 0x0006108C
		private static void SetDictionary(ServiceUserConfiguration serviceUserConfiguration, UserConfiguration userConfiguration)
		{
			if (serviceUserConfiguration.Dictionary == null)
			{
				return;
			}
			ConfigurationDictionary dictionary = UserConfigurationCommandBase<RequestType, SingleItemType>.GetDictionary(userConfiguration);
			dictionary.Clear();
			IDictionary dictionary2 = dictionary;
			foreach (UserConfigurationDictionaryEntry userConfigurationDictionaryEntry in serviceUserConfiguration.Dictionary)
			{
				object key = UserConfigurationCommandBase<RequestType, SingleItemType>.ConstructDictionaryKey(userConfigurationDictionaryEntry.DictionaryKey, dictionary2);
				object value = UserConfigurationCommandBase<RequestType, SingleItemType>.ConstructDictionaryValue(userConfigurationDictionaryEntry.DictionaryValue);
				try
				{
					dictionary2.Add(key, value);
				}
				catch (NotSupportedException innerException)
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)2517173182U, innerException);
				}
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00062F1C File Offset: 0x0006111C
		private static void UpdateDictionary(ServiceUserConfiguration serviceUserConfiguration, UserConfiguration userConfiguration)
		{
			if (serviceUserConfiguration.Dictionary == null || serviceUserConfiguration.Dictionary.Length == 0)
			{
				return;
			}
			ConfigurationDictionary dictionary = UserConfigurationCommandBase<RequestType, SingleItemType>.GetDictionary(userConfiguration);
			IDictionary dictionary2 = dictionary;
			foreach (UserConfigurationDictionaryEntry userConfigurationDictionaryEntry in serviceUserConfiguration.Dictionary)
			{
				object key = UserConfigurationCommandBase<RequestType, SingleItemType>.ConstructDictionaryObject(userConfigurationDictionaryEntry.DictionaryKey);
				object value = UserConfigurationCommandBase<RequestType, SingleItemType>.ConstructDictionaryValue(userConfigurationDictionaryEntry.DictionaryValue);
				try
				{
					if (dictionary2.Contains(key))
					{
						dictionary2[key] = value;
					}
					else
					{
						dictionary2.Add(key, value);
					}
				}
				catch (NotSupportedException innerException)
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)2517173182U, innerException);
				}
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00062FC4 File Offset: 0x000611C4
		private static object ConstructDictionaryKey(UserConfigurationDictionaryObject userConfigurationDictionaryKey, IDictionary configurationIDictionary)
		{
			object obj = UserConfigurationCommandBase<RequestType, SingleItemType>.ConstructDictionaryObject(userConfigurationDictionaryKey);
			if (configurationIDictionary.Contains(obj))
			{
				string dictionaryKey;
				if (userConfigurationDictionaryKey.Value.Length > 1)
				{
					dictionaryKey = userConfigurationDictionaryKey.Value[0];
				}
				else
				{
					dictionaryKey = string.Join(" ", userConfigurationDictionaryKey.Value);
				}
				throw InvalidValueForPropertyException.CreateDuplicateDictionaryKeyError((CoreResources.IDs)2578390262U, dictionaryKey, null);
			}
			return obj;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0006301C File Offset: 0x0006121C
		private static object ConstructDictionaryValue(UserConfigurationDictionaryObject userConfigurationDictionaryValue)
		{
			object result;
			if (userConfigurationDictionaryValue == null)
			{
				result = null;
			}
			else
			{
				result = UserConfigurationCommandBase<RequestType, SingleItemType>.ConstructDictionaryObject(userConfigurationDictionaryValue);
			}
			return result;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00063038 File Offset: 0x00061238
		private static object ConstructDictionaryObject(UserConfigurationDictionaryObject userConfigurationDictionaryObject)
		{
			object result = null;
			if (userConfigurationDictionaryObject.Value == null || userConfigurationDictionaryObject.Value.Length == 0)
			{
				throw new InvalidValueForPropertyException(CoreResources.IDs.ErrorInvalidValueForProperty);
			}
			if (userConfigurationDictionaryObject.Type != UserConfigurationDictionaryObjectType.StringArray && userConfigurationDictionaryObject.Value.Length > 1)
			{
				throw new InvalidValueForPropertyException(CoreResources.IDs.ErrorInvalidValueForPropertyStringArrayDictionaryKey);
			}
			try
			{
				switch (userConfigurationDictionaryObject.Type)
				{
				case UserConfigurationDictionaryObjectType.DateTime:
					result = ExDateTimeConverter.Parse(userConfigurationDictionaryObject.Value[0]);
					break;
				case UserConfigurationDictionaryObjectType.Boolean:
					result = BooleanConverter.Parse(userConfigurationDictionaryObject.Value[0]);
					break;
				case UserConfigurationDictionaryObjectType.Byte:
					result = ByteConverter.Parse(userConfigurationDictionaryObject.Value[0]);
					break;
				case UserConfigurationDictionaryObjectType.String:
					result = userConfigurationDictionaryObject.Value[0];
					break;
				case UserConfigurationDictionaryObjectType.Integer32:
					result = IntConverter.Parse(userConfigurationDictionaryObject.Value[0]);
					break;
				case UserConfigurationDictionaryObjectType.UnsignedInteger32:
					result = UIntConverter.Parse(userConfigurationDictionaryObject.Value[0]);
					break;
				case UserConfigurationDictionaryObjectType.Integer64:
					result = LongConverter.Parse(userConfigurationDictionaryObject.Value[0]);
					break;
				case UserConfigurationDictionaryObjectType.UnsignedInteger64:
					result = ULongConverter.Parse(userConfigurationDictionaryObject.Value[0]);
					break;
				case UserConfigurationDictionaryObjectType.StringArray:
					result = userConfigurationDictionaryObject.Value;
					break;
				case UserConfigurationDictionaryObjectType.ByteArray:
					result = Base64StringConverter.Parse(userConfigurationDictionaryObject.Value[0]);
					break;
				}
			}
			catch (FormatException exception)
			{
				UserConfigurationCommandBase<RequestType, SingleItemType>.ThrowInvalidValueException(userConfigurationDictionaryObject, exception);
			}
			catch (OverflowException exception2)
			{
				UserConfigurationCommandBase<RequestType, SingleItemType>.ThrowInvalidValueException(userConfigurationDictionaryObject, exception2);
			}
			catch (ArgumentNullException exception3)
			{
				UserConfigurationCommandBase<RequestType, SingleItemType>.ThrowInvalidValueException(userConfigurationDictionaryObject, exception3);
			}
			return result;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x000631D0 File Offset: 0x000613D0
		private static void ThrowInvalidValueException(UserConfigurationDictionaryObject userConfigurationDictionaryObject, Exception exception)
		{
			ExTraceGlobals.ExceptionTracer.TraceError<string, UserConfigurationDictionaryObjectType, Exception>(0L, "Can't construct dictionary object with value: {0} type: {1} exception: {2}", userConfigurationDictionaryObject.Value[0], userConfigurationDictionaryObject.Type, exception);
			throw InvalidValueForPropertyException.CreateConversionError(CoreResources.IDs.ErrorInvalidValueForPropertyKeyValueConversion, userConfigurationDictionaryObject.Value[0], userConfigurationDictionaryObject.Type.ToString(), exception);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00063228 File Offset: 0x00061428
		private static void SetXmlStream(ServiceUserConfiguration serviceUserConfiguration, UserConfiguration userConfiguration)
		{
			if (serviceUserConfiguration.XmlData == null)
			{
				return;
			}
			using (Stream xmlStream = UserConfigurationCommandBase<RequestType, SingleItemType>.GetXmlStream(userConfiguration))
			{
				UserConfigurationCommandBase<RequestType, SingleItemType>.SetStreamPropertyFromBase64String(serviceUserConfiguration.XmlData, xmlStream, (CoreResources.IDs)2643780243U);
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00063278 File Offset: 0x00061478
		private static void SetStream(ServiceUserConfiguration serviceUserConfiguration, UserConfiguration userConfiguration)
		{
			if (serviceUserConfiguration.BinaryData == null)
			{
				return;
			}
			using (Stream stream = UserConfigurationCommandBase<RequestType, SingleItemType>.GetStream(userConfiguration))
			{
				UserConfigurationCommandBase<RequestType, SingleItemType>.SetStreamPropertyFromBase64String(serviceUserConfiguration.BinaryData, stream, CoreResources.IDs.ErrorInvalidValueForPropertyBinaryData);
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x000632C8 File Offset: 0x000614C8
		private static void SetStreamPropertyFromBase64String(string propertyAsBase64String, Stream stream, Enum invalidPropertyValueError)
		{
			byte[] array;
			try
			{
				array = Base64StringConverter.Parse(propertyAsBase64String);
			}
			catch (FormatException ex)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<string, FormatException>(0L, "Can't parse base64 string {0} Exception: {1}", propertyAsBase64String, ex);
				throw new InvalidValueForPropertyException(invalidPropertyValueError, ex);
			}
			stream.SetLength(0L);
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x020002C9 RID: 713
		protected class UserConfigurationName
		{
			// Token: 0x060013CF RID: 5071 RVA: 0x00063320 File Offset: 0x00061520
			internal UserConfigurationName(string name, StoreId folderId, MailboxSession mailboxSession)
			{
				if (!UserConfigurationManager.IsValidName(name))
				{
					throw new InvalidValueForPropertyException((CoreResources.IDs)2744667914U);
				}
				this.name = name;
				this.folderId = folderId;
				this.mailboxSession = mailboxSession;
			}

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00063355 File Offset: 0x00061555
			internal string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0006335D File Offset: 0x0006155D
			internal StoreId FolderId
			{
				get
				{
					return this.folderId;
				}
			}

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00063365 File Offset: 0x00061565
			internal MailboxSession MailboxSession
			{
				get
				{
					return this.mailboxSession;
				}
			}

			// Token: 0x04000D70 RID: 3440
			private string name;

			// Token: 0x04000D71 RID: 3441
			private StoreId folderId;

			// Token: 0x04000D72 RID: 3442
			private MailboxSession mailboxSession;
		}
	}
}
