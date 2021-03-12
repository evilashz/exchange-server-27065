using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000C6 RID: 198
	internal abstract class PropertyCommand
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0001D120 File Offset: 0x0001B320
		internal PropertyCommand(CommandContext commandContext)
		{
			this.commandContext = commandContext;
			this.xmlLocalName = commandContext.PropertyInformation.LocalName;
			this.xmlNamespaceUri = commandContext.PropertyInformation.NamespaceUri;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001D151 File Offset: 0x0001B351
		public static bool InMemoryProcessOnly
		{
			get
			{
				return PropertyCommand.inMemoryProcessOnly;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0001D158 File Offset: 0x0001B358
		public virtual bool ToServiceObjectRequiresMailboxAccess
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001D15C File Offset: 0x0001B35C
		public static void ToServiceObjectInMemoryOnly(Action process)
		{
			try
			{
				PropertyCommand.inMemoryProcessOnly = true;
				process();
			}
			finally
			{
				PropertyCommand.inMemoryProcessOnly = false;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001D190 File Offset: 0x0001B390
		public static object GetPropertyValueFromStoreObject(StoreObject storeObject, PropertyDefinition propertyDefinition)
		{
			object obj = storeObject.TryGetProperty(propertyDefinition);
			PropertyError propertyError = obj as PropertyError;
			if (propertyError == null)
			{
				return obj;
			}
			StorePropertyDefinition storePropertyDefinition = (StorePropertyDefinition)propertyDefinition;
			if ((propertyError.PropertyErrorCode == PropertyErrorCode.NotEnoughMemory || propertyError.PropertyErrorCode == PropertyErrorCode.RequireStreamed) && storePropertyDefinition.SpecifiedWith != PropertyTypeSpecifier.Calculated)
			{
				try
				{
					using (Stream stream = storeObject.OpenPropertyStream(propertyDefinition, PropertyOpenMode.ReadOnly))
					{
						byte[] array = new byte[stream.Length];
						stream.Read(array, 0, array.Length);
						return PropertyCommand.DeserializePropertyStream(propertyDefinition.Type, array);
					}
				}
				catch (NotSupportedException)
				{
					throw new DataSizeLimitException(SearchSchemaMap.GetPropertyPath(propertyDefinition));
				}
				catch (InvalidOperationException)
				{
					throw new DataSizeLimitException(SearchSchemaMap.GetPropertyPath(propertyDefinition));
				}
			}
			throw PropertyError.ToException(new PropertyError[]
			{
				propertyError
			});
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001D270 File Offset: 0x0001B470
		public void Update()
		{
			UpdateCommandSettings commandSettings = this.GetCommandSettings<UpdateCommandSettings>();
			SetPropertyUpdate setPropertyUpdate = null;
			DeletePropertyUpdate deletePropertyUpdate = null;
			AppendPropertyUpdate appendPropertyUpdate = null;
			if (UpdatePropertyList.TryGetPropertyUpdate<AppendPropertyUpdate>(commandSettings.PropertyUpdate, out appendPropertyUpdate))
			{
				this.AppendUpdate(appendPropertyUpdate, commandSettings);
				return;
			}
			if (UpdatePropertyList.TryGetPropertyUpdate<SetPropertyUpdate>(commandSettings.PropertyUpdate, out setPropertyUpdate))
			{
				this.SetUpdate(setPropertyUpdate, commandSettings);
				return;
			}
			if (UpdatePropertyList.TryGetPropertyUpdate<DeletePropertyUpdate>(commandSettings.PropertyUpdate, out deletePropertyUpdate))
			{
				this.DeleteUpdate(deletePropertyUpdate, commandSettings);
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001D2D1 File Offset: 0x0001B4D1
		public virtual void AppendUpdate(AppendPropertyUpdate appendPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0001D2D3 File Offset: 0x0001B4D3
		public virtual void DeleteUpdate(DeletePropertyUpdate deletePropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001D2D5 File Offset: 0x0001B4D5
		public virtual void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001D2D7 File Offset: 0x0001B4D7
		public virtual void SetPhase2()
		{
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001D2D9 File Offset: 0x0001B4D9
		public virtual void SetPhase3()
		{
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001D2DB File Offset: 0x0001B4DB
		public virtual void PostUpdate()
		{
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		public void SetPropertyValueOnStoreObject(StoreObject storeObject, PropertyDefinition propertyDefinition, object value)
		{
			try
			{
				this.SetStoreObjectProperty(storeObject, propertyDefinition, value);
			}
			catch (PropertyErrorException ex)
			{
				StorePropertyDefinition storePropertyDefinition = (StorePropertyDefinition)propertyDefinition;
				if (ex.PropertyErrors.Length == 1 && storePropertyDefinition.SpecifiedWith != PropertyTypeSpecifier.Calculated && (ex.PropertyErrors[0].PropertyErrorCode == PropertyErrorCode.NotEnoughMemory || ex.PropertyErrors[0].PropertyErrorCode == PropertyErrorCode.RequireStreamed))
				{
					byte[] array = PropertyCommand.SerializePropertyStream(value);
					try
					{
						using (Stream stream = storeObject.OpenPropertyStream(propertyDefinition, PropertyOpenMode.Modify))
						{
							stream.Write(array, 0, array.Length);
						}
						goto IL_90;
					}
					catch (NotSupportedException)
					{
						throw new DataSizeLimitException(SearchSchemaMap.GetPropertyPath(propertyDefinition));
					}
					catch (InvalidOperationException)
					{
						throw new DataSizeLimitException(SearchSchemaMap.GetPropertyPath(propertyDefinition));
					}
					goto IL_8E;
					IL_90:
					return;
				}
				IL_8E:
				throw;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001D3B4 File Offset: 0x0001B5B4
		public override string ToString()
		{
			return string.Format("Name: {0}, Type: {1}", string.IsNullOrEmpty(this.xmlLocalName) ? "<NULL>" : this.xmlLocalName, base.GetType().FullName);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001D3E8 File Offset: 0x0001B5E8
		internal static bool TryGetValueFromPropertyBag<T>(IDictionary<PropertyDefinition, object> propertyBag, PropertyDefinition key, out T value)
		{
			object obj = null;
			value = default(T);
			if (propertyBag.TryGetValue(key, out obj) && obj is T)
			{
				value = (T)((object)obj);
				return true;
			}
			return false;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001D420 File Offset: 0x0001B620
		protected static bool StorePropertyExists(StoreObject storeObject, PropertyDefinition propertyDefinition)
		{
			PropertyError propertyError = storeObject.TryGetProperty(propertyDefinition) as PropertyError;
			if (propertyError != null)
			{
				PropertyErrorCode propertyErrorCode = propertyError.PropertyErrorCode;
				switch (propertyErrorCode)
				{
				case PropertyErrorCode.NotFound:
				case PropertyErrorCode.IncorrectValueType:
				case PropertyErrorCode.GetCalculatedPropertyError:
					return false;
				case PropertyErrorCode.NotEnoughMemory:
					break;
				case PropertyErrorCode.NullValue:
				case PropertyErrorCode.SetCalculatedPropertyError:
				case PropertyErrorCode.SetStoreComputedPropertyError:
					goto IL_4C;
				default:
					if (propertyErrorCode != PropertyErrorCode.RequireStreamed)
					{
						goto IL_4C;
					}
					break;
				}
				return true;
				IL_4C:
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<PropertyErrorCode, string>(0L, "Store error inside StorePropertyExists(), error code = {0} description = {1}", propertyError.PropertyErrorCode, propertyError.PropertyErrorDescription);
				throw new PropertyRequestFailedException((storeObject is Folder) ? ((CoreResources.IDs)2370747299U) : CoreResources.IDs.ErrorItemPropertyRequestFailed, SearchSchemaMap.GetPropertyPath(propertyDefinition));
			}
			return true;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		protected static void PreventSentMessageUpdate(CommandContext commandContext)
		{
			UpdateCommandSettings updateCommandSettings = commandContext.CommandSettings as UpdateCommandSettings;
			if (updateCommandSettings != null)
			{
				MessageItem messageItem = updateCommandSettings.StoreObject as MessageItem;
				if (messageItem != null && !messageItem.IsDraft)
				{
					throw new InvalidPropertyUpdateSentMessageException(commandContext.PropertyInformation.PropertyPath);
				}
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001D504 File Offset: 0x0001B704
		protected T GetCommandSettings<T>() where T : CommandSettings
		{
			return this.commandContext.CommandSettings as T;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001D528 File Offset: 0x0001B728
		internal static SingleRecipientType CreateRecipientFromParticipant(ParticipantInformation participantInformation)
		{
			SingleRecipientType singleRecipientType = new SingleRecipientType();
			singleRecipientType.Mailbox = new EmailAddressWrapper();
			singleRecipientType.Mailbox.Name = participantInformation.DisplayName;
			singleRecipientType.Mailbox.EmailAddress = participantInformation.EmailAddress;
			singleRecipientType.Mailbox.RoutingType = participantInformation.RoutingType;
			singleRecipientType.Mailbox.SipUri = participantInformation.SipUri;
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				singleRecipientType.Mailbox.MailboxType = participantInformation.MailboxType.ToString();
			}
			return singleRecipientType;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		internal static SingleRecipientType CreateOneOffRecipientFromParticipant(IParticipant participant)
		{
			return new SingleRecipientType
			{
				Mailbox = new EmailAddressWrapper(),
				Mailbox = 
				{
					Name = participant.DisplayName,
					EmailAddress = participant.EmailAddress,
					RoutingType = participant.RoutingType,
					SipUri = participant.SipUri,
					MailboxType = MailboxHelper.MailboxTypeType.OneOff.ToString()
				}
			};
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001D634 File Offset: 0x0001B834
		internal static SingleRecipientType CreateOneOffRecipientFromParticipantSmtpAddress(IParticipant participant)
		{
			return new SingleRecipientType
			{
				Mailbox = new EmailAddressWrapper(),
				Mailbox = 
				{
					Name = participant.DisplayName,
					EmailAddress = participant.SmtpEmailAddress,
					RoutingType = "SMTP",
					SipUri = participant.SipUri,
					MailboxType = MailboxHelper.MailboxTypeType.OneOff.ToString()
				}
			};
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		protected SingleRecipientType CreateRecipientFromParticipant(ParticipantInformation participantInformation, StoreObject storeObject)
		{
			SingleRecipientType singleRecipientType = PropertyCommand.CreateRecipientFromParticipant(participantInformation);
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010) || participantInformation.RoutingType == "MAPIPDL")
			{
				StoreParticipantOrigin storeParticipantOrigin = participantInformation.Origin as StoreParticipantOrigin;
				if (storeParticipantOrigin != null && storeParticipantOrigin.OriginItemId != null)
				{
					MailboxSession mailboxSession = storeObject.Session as MailboxSession;
					if (mailboxSession != null && !IdConverter.IsFromPublicStore(storeParticipantOrigin.OriginItemId))
					{
						MailboxId mailboxId = new MailboxId(mailboxSession);
						ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeParticipantOrigin.OriginItemId, mailboxId, null);
						singleRecipientType.Mailbox.ItemId = new ItemId(concatenatedId.Id, concatenatedId.ChangeKey);
					}
				}
			}
			return singleRecipientType;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001D74C File Offset: 0x0001B94C
		protected ItemId GetParticipantItemId(ParticipantInformation participantInformation, StoreObject storeObject)
		{
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010) || participantInformation.RoutingType == "MAPIPDL")
			{
				StoreParticipantOrigin storeParticipantOrigin = participantInformation.Origin as StoreParticipantOrigin;
				if (storeParticipantOrigin != null && storeParticipantOrigin.OriginItemId != null)
				{
					MailboxSession mailboxSession = storeObject.Session as MailboxSession;
					if (mailboxSession != null && !IdConverter.IsFromPublicStore(storeParticipantOrigin.OriginItemId))
					{
						MailboxId mailboxId = new MailboxId(mailboxSession);
						ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeParticipantOrigin.OriginItemId, mailboxId, null);
						return new ItemId
						{
							Id = concatenatedId.Id,
							ChangeKey = concatenatedId.ChangeKey
						};
					}
				}
			}
			return null;
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001D7EB File Offset: 0x0001B9EB
		protected virtual Participant GetParticipantFromAddress(Item item, EmailAddressWrapper address)
		{
			return MailboxHelper.GetParticipantFromAddress(address);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		protected Participant GetParticipantOrDLFromAddress(EmailAddressWrapper address, StoreObject storeObject)
		{
			Participant result = null;
			if (address.ItemId == null)
			{
				result = MailboxHelper.GetParticipantFromAddress(address);
			}
			else
			{
				if (!(storeObject.Session is MailboxSession))
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)3795663900U);
				}
				StoreObjectType storeObjectType = StoreObjectType.Unknown;
				IdAndSession idAndSession = null;
				try
				{
					idAndSession = this.commandContext.IdConverter.ConvertItemIdToIdAndSessionReadOnly(address.ItemId);
					storeObjectType = IdConverter.GetAsStoreObjectId(idAndSession.Id).ObjectType;
				}
				catch (ObjectNotFoundException)
				{
					if (address.MailboxType == MailboxHelper.MailboxTypeType.PrivateDL.ToString())
					{
						throw;
					}
					storeObjectType = StoreObjectType.Contact;
				}
				switch (storeObjectType)
				{
				case StoreObjectType.Contact:
					break;
				case StoreObjectType.DistributionList:
					using (DistributionList distributionList = (DistributionList)ServiceCommandBase.GetXsoItem(storeObject.Session, idAndSession.Id, new PropertyDefinition[0]))
					{
						return distributionList.GetAsParticipant();
					}
					break;
				default:
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, GetParticipantOrDLFromAddressMetadata.ObjectType, storeObjectType);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, GetParticipantOrDLFromAddressMetadata.EmailAddress, address.EmailAddress);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, GetParticipantOrDLFromAddressMetadata.Name, address.Name);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, GetParticipantOrDLFromAddressMetadata.OriginalDisplayName, address.OriginalDisplayName);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, GetParticipantOrDLFromAddressMetadata.MailboxType, address.MailboxType);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, GetParticipantOrDLFromAddressMetadata.ItemId, address.ItemId);
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCannotUsePersonalContactsAsRecipientsOrAttendees);
				}
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2013))
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCannotUsePersonalContactsAsRecipientsOrAttendees);
				}
				result = MailboxHelper.GetParticipantFromAddress(address);
			}
			return result;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001D9AC File Offset: 0x0001BBAC
		protected virtual void SetStoreObjectProperty(StoreObject storeObject, PropertyDefinition propertyDefinition, object value)
		{
			storeObject[propertyDefinition] = value;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001D9B6 File Offset: 0x0001BBB6
		protected void ValidateDataSize(long dataSize)
		{
			if (dataSize > 2147483647L)
			{
				throw new DataSizeLimitException(this.commandContext.PropertyInformation.PropertyPath);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
		protected void DeleteProperties(StoreObject storeObject, PropertyPath propertyPath, params PropertyDefinition[] propertyDefinitions)
		{
			try
			{
				storeObject.DeleteProperties(propertyDefinitions);
			}
			catch (NotSupportedException innerException)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError(0L, "NotSupportedException for PropertyPath: " + propertyPath);
				throw new InvalidPropertyDeleteException(propertyPath, innerException);
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001DA20 File Offset: 0x0001BC20
		private static byte[] SerializePropertyStream(object valueToSerialize)
		{
			string text = valueToSerialize as string;
			string s = text;
			if (text != null)
			{
				if (CallContext.Current.EncodeStringProperties && ExchangeVersion.Current.Supports(ExchangeVersionType.Exchange2012))
				{
					s = Util.EncodeForAntiXSS(text);
				}
				return Encoding.Unicode.GetBytes(s);
			}
			byte[] array = valueToSerialize as byte[];
			if (array != null)
			{
				return array;
			}
			return null;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001DA72 File Offset: 0x0001BC72
		private static object DeserializePropertyStream(Type type, byte[] bytes)
		{
			if (type == typeof(string))
			{
				return Encoding.Unicode.GetString(bytes, 0, bytes.Length);
			}
			if (type == typeof(byte[]))
			{
				return bytes;
			}
			return null;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001DAAB File Offset: 0x0001BCAB
		protected static void CreateXmlAttribute(XmlElement parentElement, string attributeName, string attributeValue)
		{
			ServiceXml.CreateAttribute(parentElement, attributeName, attributeValue);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		protected XmlElement CreateXmlElement(XmlElement parentElement, string localName)
		{
			return ServiceXml.CreateElement(parentElement, localName, this.xmlNamespaceUri);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001DAD4 File Offset: 0x0001BCD4
		protected XmlElement CreateXmlTextElement(XmlElement parentElement, string localName, string textValue)
		{
			return ServiceXml.CreateTextElement(parentElement, localName, textValue, this.xmlNamespaceUri);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		protected XmlElement CreateXmlTextElement(XmlElement parentElement, string localName, XmlText textNode)
		{
			return ServiceXml.CreateTextElement(parentElement, localName, textNode, this.xmlNamespaceUri);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001DAF4 File Offset: 0x0001BCF4
		protected XmlElement CreateXmlTextElementOptionally(XmlElement parentElement, string localName, string textValue)
		{
			if (string.IsNullOrEmpty(textValue))
			{
				return null;
			}
			return ServiceXml.CreateTextElement(parentElement, localName, textValue, this.xmlNamespaceUri);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001DB10 File Offset: 0x0001BD10
		protected XmlElement CreateParticipantXml(XmlElement parentElement, ParticipantInformation participantInformation)
		{
			XmlElement xmlElement = this.CreateXmlElement(parentElement, "Mailbox");
			this.CreateXmlTextElement(xmlElement, "Name", participantInformation.DisplayName);
			this.CreateXmlTextElementOptionally(xmlElement, "EmailAddress", participantInformation.EmailAddress);
			this.CreateXmlTextElementOptionally(xmlElement, "RoutingType", participantInformation.RoutingType);
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				MailboxHelper.MailboxTypeType mailboxType = participantInformation.MailboxType;
				this.CreateXmlTextElementOptionally(xmlElement, "MailboxType", mailboxType.ToString());
			}
			return xmlElement;
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001DB94 File Offset: 0x0001BD94
		protected void CreateParticipantOrDLXml(XmlElement parentElement, ParticipantInformation participantInformation, StoreObject storeObject)
		{
			if (participantInformation == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "Participant is null. storeObject.ClassName={0};", storeObject.ClassName);
				return;
			}
			XmlElement idParentElement = this.CreateParticipantXml(parentElement, participantInformation);
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010) || participantInformation.RoutingType == "MAPIPDL")
			{
				StoreParticipantOrigin storeParticipantOrigin = participantInformation.Origin as StoreParticipantOrigin;
				if (storeParticipantOrigin != null && storeParticipantOrigin.OriginItemId != null)
				{
					MailboxSession mailboxSession = storeObject.Session as MailboxSession;
					if (mailboxSession != null && !IdConverter.IsFromPublicStore(storeParticipantOrigin.OriginItemId))
					{
						MailboxId mailboxId = new MailboxId(mailboxSession);
						IdConverter.CreateStoreIdXml(idParentElement, storeParticipantOrigin.OriginItemId, mailboxId, "ItemId");
					}
				}
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		protected Participant GetParticipantFromXml(XmlElement parentElement)
		{
			XmlElement xmlElement = parentElement["Name", "http://schemas.microsoft.com/exchange/services/2006/types"];
			string displayName;
			if (xmlElement == null)
			{
				displayName = string.Empty;
			}
			else
			{
				displayName = ServiceXml.GetXmlTextNodeValue(xmlElement);
			}
			XmlElement xmlElement2 = parentElement["EmailAddress", "http://schemas.microsoft.com/exchange/services/2006/types"];
			if (xmlElement2 == null)
			{
				throw new MissingInformationEmailAddressException();
			}
			string xmlTextNodeValue = ServiceXml.GetXmlTextNodeValue(xmlElement2);
			XmlElement xmlElement3 = parentElement["RoutingType", "http://schemas.microsoft.com/exchange/services/2006/types"];
			string text;
			if (xmlElement3 == null)
			{
				text = string.Empty;
			}
			else
			{
				text = ServiceXml.GetXmlTextNodeValue(xmlElement3);
				try
				{
					new CustomProxyAddressPrefix(text);
				}
				catch (ArgumentException ex)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, string>((long)this.GetHashCode(), "Invalid routing type: '{0}'.  ArgumentException encountered: {1}", text, ex.Message);
					throw new MalformedRoutingTypeException(this.commandContext.PropertyInformation.PropertyPath, ex);
				}
			}
			Participant participant = new Participant(displayName, xmlTextNodeValue, text);
			Participant participant2 = MailboxHelper.TryConvertTo(participant, "EX");
			if (null != participant2)
			{
				return participant2;
			}
			return participant;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001DD30 File Offset: 0x0001BF30
		protected Participant GetParticipantOrDLFromXml(XmlElement parentElement, StoreObject storeObject)
		{
			XmlElement xmlElement = parentElement["ItemId", "http://schemas.microsoft.com/exchange/services/2006/types"];
			Participant result = null;
			if (xmlElement == null)
			{
				result = this.GetParticipantFromXml(parentElement);
			}
			else
			{
				if (!(storeObject.Session is MailboxSession))
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)3795663900U);
				}
				StoreId id = IdConverter.ConvertXmlToStoreId(xmlElement, (MailboxSession)storeObject.Session, BasicTypes.Item);
				switch (IdConverter.GetAsStoreObjectId(id).ObjectType)
				{
				case StoreObjectType.Contact:
					break;
				case StoreObjectType.DistributionList:
					using (DistributionList distributionList = (DistributionList)ServiceCommandBase.GetXsoItem(storeObject.Session, id, new PropertyDefinition[0]))
					{
						return distributionList.GetAsParticipant();
					}
					break;
				default:
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCannotUsePersonalContactsAsRecipientsOrAttendees);
				}
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2013))
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCannotUsePersonalContactsAsRecipientsOrAttendees);
				}
				result = this.GetParticipantFromXml(parentElement);
			}
			return result;
		}

		// Token: 0x04000692 RID: 1682
		private const int StreamReadSize = 1024;

		// Token: 0x04000693 RID: 1683
		[ThreadStatic]
		private static bool inMemoryProcessOnly;

		// Token: 0x04000694 RID: 1684
		protected string xmlLocalName;

		// Token: 0x04000695 RID: 1685
		protected string xmlNamespaceUri;

		// Token: 0x04000696 RID: 1686
		protected CommandContext commandContext;

		// Token: 0x020000C7 RID: 199
		// (Invoke) Token: 0x0600059F RID: 1439
		public delegate IPropertyCommand CreatePropertyCommand(CommandContext commandContext);
	}
}
