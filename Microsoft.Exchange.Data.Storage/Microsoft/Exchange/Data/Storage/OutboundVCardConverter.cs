using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.ContentTypes.vCard;
using Microsoft.Exchange.Data.Mime.Encoders;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200057D RID: 1405
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class OutboundVCardConverter
	{
		// Token: 0x06003A2F RID: 14895 RVA: 0x000EEFB8 File Offset: 0x000ED1B8
		static OutboundVCardConverter()
		{
			OutboundVCardConverter.exporters.Add(OutboundVCardConverter.GenericExporter.Instance);
			OutboundVCardConverter.exporters.Add(OutboundVCardConverter.FormattedNameExporter.Instance);
			OutboundVCardConverter.exporters.Add(OutboundVCardConverter.NameExporter.Instance);
			OutboundVCardConverter.exporters.Add(OutboundVCardConverter.PhotoExporter.Instance);
			OutboundVCardConverter.exporters.Add(OutboundVCardConverter.EmailExporter.Instance);
			OutboundVCardConverter.exporters.Add(OutboundVCardConverter.NoteExporter.Instance);
			OutboundVCardConverter.exporters.Add(OutboundVCardConverter.OrgExporter.Instance);
			OutboundVCardConverter.exporters.Add(OutboundVCardConverter.ClassExporter.Instance);
			OutboundVCardConverter.AddressExporter.RegisterAll(OutboundVCardConverter.exporters);
			OutboundVCardConverter.TypedPropertyStringExporter.RegisterAll(OutboundVCardConverter.exporters);
			OutboundVCardConverter.MultiStringExporter.RegisterAll(OutboundVCardConverter.exporters);
			OutboundVCardConverter.DatePropExporter.RegisterAll(OutboundVCardConverter.exporters);
			OutboundVCardConverter.StreamTextExporter.RegisterAll(OutboundVCardConverter.exporters);
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000EF07C File Offset: 0x000ED27C
		internal static void Convert(Stream dataStream, Encoding encoding, Contact contact, OutboundConversionOptions options, ConversionLimitsTracker limitsTracker)
		{
			Util.ThrowOnNullArgument(options, "options");
			Util.ThrowOnNullOrEmptyArgument(options.ImceaEncapsulationDomain, "options.ImceaEncapsulationDomain");
			ContactWriter contactWriter = new ContactWriter(dataStream, encoding);
			contactWriter.StartVCard();
			OutboundVCardConverter.PropertyExporter.Context context = new OutboundVCardConverter.PropertyExporter.Context(encoding, options, limitsTracker);
			foreach (OutboundVCardConverter.PropertyExporter propertyExporter in OutboundVCardConverter.exporters)
			{
				propertyExporter.Export(contactWriter, contact, context);
			}
			contactWriter.EndVCard();
		}

		// Token: 0x04001F38 RID: 7992
		private static readonly List<OutboundVCardConverter.PropertyExporter> exporters = new List<OutboundVCardConverter.PropertyExporter>();

		// Token: 0x0200057E RID: 1406
		private abstract class PropertyExporter
		{
			// Token: 0x06003A31 RID: 14897
			public abstract void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context);

			// Token: 0x0200057F RID: 1407
			internal class Context
			{
				// Token: 0x06003A33 RID: 14899 RVA: 0x000EF114 File Offset: 0x000ED314
				internal Context(Encoding encoding, OutboundConversionOptions options, ConversionLimitsTracker limitsTracker)
				{
					this.Encoding = encoding;
					this.AddressCache = new OutboundAddressCache(options, limitsTracker);
					this.Options = options;
				}

				// Token: 0x04001F39 RID: 7993
				internal readonly OutboundAddressCache AddressCache;

				// Token: 0x04001F3A RID: 7994
				internal readonly Encoding Encoding;

				// Token: 0x04001F3B RID: 7995
				internal readonly OutboundConversionOptions Options;
			}
		}

		// Token: 0x02000580 RID: 1408
		private class GenericExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A34 RID: 14900 RVA: 0x000EF137 File Offset: 0x000ED337
			private GenericExporter()
			{
			}

			// Token: 0x06003A35 RID: 14901 RVA: 0x000EF13F File Offset: 0x000ED33F
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				writer.WriteProperty(PropertyId.Profile, "VCARD");
				writer.WriteProperty(PropertyId.Version, "3.0");
				writer.WriteProperty(PropertyId.Mailer, "Microsoft Exchange");
				writer.WriteProperty(PropertyId.ProductId, "Microsoft Exchange");
			}

			// Token: 0x04001F3C RID: 7996
			public static readonly OutboundVCardConverter.GenericExporter Instance = new OutboundVCardConverter.GenericExporter();
		}

		// Token: 0x02000581 RID: 1409
		private class FormattedNameExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A37 RID: 14903 RVA: 0x000EF180 File Offset: 0x000ED380
			private FormattedNameExporter()
			{
			}

			// Token: 0x06003A38 RID: 14904 RVA: 0x000EF188 File Offset: 0x000ED388
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				string text = contact.TryGetProperty(ContactSchema.FullName) as string;
				if (text == null)
				{
					text = (contact.TryGetProperty(ItemSchema.NormalizedSubject) as string);
					if (text == null)
					{
						text = string.Empty;
					}
				}
				writer.WriteProperty(PropertyId.CommonName, text);
			}

			// Token: 0x04001F3D RID: 7997
			public static readonly OutboundVCardConverter.FormattedNameExporter Instance = new OutboundVCardConverter.FormattedNameExporter();
		}

		// Token: 0x02000582 RID: 1410
		private class NameExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A3A RID: 14906 RVA: 0x000EF1D7 File Offset: 0x000ED3D7
			private NameExporter()
			{
			}

			// Token: 0x06003A3B RID: 14907 RVA: 0x000EF1E0 File Offset: 0x000ED3E0
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				writer.StartProperty(PropertyId.StructuredName);
				for (int i = 0; i < OutboundVCardConverter.NameExporter.sourceProperties.Length; i++)
				{
					string text = contact.TryGetProperty(OutboundVCardConverter.NameExporter.sourceProperties[i]) as string;
					if (text == null)
					{
						text = string.Empty;
					}
					writer.WritePropertyValue(text, ContactValueSeparators.Semicolon);
				}
			}

			// Token: 0x04001F3E RID: 7998
			public static readonly OutboundVCardConverter.NameExporter Instance = new OutboundVCardConverter.NameExporter();

			// Token: 0x04001F3F RID: 7999
			private static readonly PropertyDefinition[] sourceProperties = new PropertyDefinition[]
			{
				ContactSchema.Surname,
				ContactSchema.GivenName,
				ContactSchema.MiddleName,
				ContactSchema.DisplayNamePrefix,
				ContactSchema.Generation
			};
		}

		// Token: 0x02000583 RID: 1411
		private class PhotoExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A3D RID: 14909 RVA: 0x000EF278 File Offset: 0x000ED478
			static PhotoExporter()
			{
				OutboundVCardConverter.PhotoExporter.contentTypeToType.Add("image/jpeg", "JPEG");
				OutboundVCardConverter.PhotoExporter.contentTypeToType.Add("image/gif", "GIF");
				OutboundVCardConverter.PhotoExporter.contentTypeToType.Add("image/png", "PNG");
				OutboundVCardConverter.PhotoExporter.contentTypeToType.Add("image/bmp", "BMP");
			}

			// Token: 0x06003A3E RID: 14910 RVA: 0x000EF2E9 File Offset: 0x000ED4E9
			private PhotoExporter()
			{
			}

			// Token: 0x06003A3F RID: 14911 RVA: 0x000EF2F4 File Offset: 0x000ED4F4
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				foreach (AttachmentHandle handle in contact.AttachmentCollection)
				{
					using (Attachment attachment = contact.AttachmentCollection.Open(handle, null))
					{
						if (attachment.IsContactPhoto)
						{
							StreamAttachment streamAttachment = attachment as StreamAttachment;
							if (streamAttachment != null)
							{
								string text = streamAttachment.ContentType;
								if (string.IsNullOrEmpty(text))
								{
									text = streamAttachment.CalculatedContentType;
								}
								string value = null;
								if (!string.IsNullOrEmpty(text) && OutboundVCardConverter.PhotoExporter.contentTypeToType.TryGetValue(text, out value))
								{
									using (Stream stream = streamAttachment.TryGetContentStream(PropertyOpenMode.ReadOnly))
									{
										if (stream != null)
										{
											writer.StartProperty(PropertyId.Photo);
											writer.WriteParameter(ParameterId.Type, value);
											writer.WriteParameter(ParameterId.Encoding, "B");
											using (Stream stream2 = new EncoderStream(stream, new Base64Encoder(0), EncoderStreamAccess.Read))
											{
												writer.WritePropertyValue(stream2);
												break;
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x04001F40 RID: 8000
			public static readonly OutboundVCardConverter.PhotoExporter Instance = new OutboundVCardConverter.PhotoExporter();

			// Token: 0x04001F41 RID: 8001
			private static readonly Dictionary<string, string> contentTypeToType = new Dictionary<string, string>();
		}

		// Token: 0x02000584 RID: 1412
		private class EmailExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A40 RID: 14912 RVA: 0x000EF42C File Offset: 0x000ED62C
			private EmailExporter()
			{
			}

			// Token: 0x06003A41 RID: 14913 RVA: 0x000EF434 File Offset: 0x000ED634
			private void WriteParticipant(ContactWriter writer, Participant addr, OutboundConversionOptions options)
			{
				if (addr == null)
				{
					return;
				}
				string participantSmtpAddress = ItemToMimeConverter.GetParticipantSmtpAddress(addr, options);
				if (!string.IsNullOrEmpty(participantSmtpAddress))
				{
					writer.StartProperty(PropertyId.EMail);
					writer.WriteParameter(ParameterId.Type, "INTERNET");
					writer.WritePropertyValue(participantSmtpAddress);
				}
			}

			// Token: 0x06003A42 RID: 14914 RVA: 0x000EF478 File Offset: 0x000ED678
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				context.AddressCache.CopyDataFromItem(contact);
				context.AddressCache.Resolve();
				this.WriteParticipant(writer, context.AddressCache.Participants[ConversionItemParticipants.ParticipantIndex.ContactEmail1], context.Options);
				this.WriteParticipant(writer, context.AddressCache.Participants[ConversionItemParticipants.ParticipantIndex.ContactEmail2], context.Options);
				this.WriteParticipant(writer, context.AddressCache.Participants[ConversionItemParticipants.ParticipantIndex.ContactEmail3], context.Options);
			}

			// Token: 0x04001F42 RID: 8002
			public static readonly OutboundVCardConverter.EmailExporter Instance = new OutboundVCardConverter.EmailExporter();
		}

		// Token: 0x02000585 RID: 1413
		private class MultiStringExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A44 RID: 14916 RVA: 0x000EF504 File Offset: 0x000ED704
			static MultiStringExporter()
			{
				OutboundVCardConverter.MultiStringExporter.Instances.Add(new OutboundVCardConverter.MultiStringExporter(InternalSchema.Categories, "CATEGORIES"));
				OutboundVCardConverter.MultiStringExporter.Instances.Add(new OutboundVCardConverter.MultiStringExporter(InternalSchema.Children, "X-MS-CHILD"));
			}

			// Token: 0x06003A45 RID: 14917 RVA: 0x000EF544 File Offset: 0x000ED744
			public static void RegisterAll(List<OutboundVCardConverter.PropertyExporter> list)
			{
				foreach (OutboundVCardConverter.MultiStringExporter item in OutboundVCardConverter.MultiStringExporter.Instances)
				{
					list.Add(item);
				}
			}

			// Token: 0x06003A46 RID: 14918 RVA: 0x000EF598 File Offset: 0x000ED798
			private MultiStringExporter(NativeStorePropertyDefinition prop, string propName)
			{
				this.prop = prop;
				this.propName = propName;
			}

			// Token: 0x06003A47 RID: 14919 RVA: 0x000EF5B0 File Offset: 0x000ED7B0
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				string[] array = contact.TryGetProperty(this.prop) as string[];
				if (array != null && array.Length > 0)
				{
					writer.StartProperty(this.propName);
					foreach (string value in array)
					{
						writer.WritePropertyValue(value, ContactValueSeparators.Comma);
					}
				}
			}

			// Token: 0x04001F43 RID: 8003
			public static readonly List<OutboundVCardConverter.MultiStringExporter> Instances = new List<OutboundVCardConverter.MultiStringExporter>();

			// Token: 0x04001F44 RID: 8004
			private readonly NativeStorePropertyDefinition prop;

			// Token: 0x04001F45 RID: 8005
			private readonly string propName;
		}

		// Token: 0x02000586 RID: 1414
		private class DatePropExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A48 RID: 14920 RVA: 0x000EF600 File Offset: 0x000ED800
			static DatePropExporter()
			{
				OutboundVCardConverter.DatePropExporter.Instances.Add(new OutboundVCardConverter.DatePropExporter(InternalSchema.Birthday, ContactValueType.Date, "BDAY"));
				OutboundVCardConverter.DatePropExporter.Instances.Add(new OutboundVCardConverter.DatePropExporter(InternalSchema.LastModifiedTime, ContactValueType.DateTime, "REV"));
				OutboundVCardConverter.DatePropExporter.Instances.Add(new OutboundVCardConverter.DatePropExporter(InternalSchema.WeddingAnniversary, ContactValueType.Date, "X-MS-ANNIVERSARY"));
			}

			// Token: 0x06003A49 RID: 14921 RVA: 0x000EF668 File Offset: 0x000ED868
			public static void RegisterAll(List<OutboundVCardConverter.PropertyExporter> list)
			{
				foreach (OutboundVCardConverter.DatePropExporter item in OutboundVCardConverter.DatePropExporter.Instances)
				{
					list.Add(item);
				}
			}

			// Token: 0x06003A4A RID: 14922 RVA: 0x000EF6BC File Offset: 0x000ED8BC
			private DatePropExporter(NativeStorePropertyDefinition prop, ContactValueType type, string propName)
			{
				this.prop = prop;
				this.propName = propName;
				this.type = type;
			}

			// Token: 0x06003A4B RID: 14923 RVA: 0x000EF6DC File Offset: 0x000ED8DC
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				ExDateTime? exDateTime = contact.TryGetProperty(this.prop) as ExDateTime?;
				if (exDateTime != null)
				{
					writer.StartProperty(this.propName);
					writer.WriteValueTypeParameter(this.type);
					writer.WritePropertyValue((DateTime)exDateTime.Value.ToUtc());
				}
			}

			// Token: 0x04001F46 RID: 8006
			public static readonly List<OutboundVCardConverter.DatePropExporter> Instances = new List<OutboundVCardConverter.DatePropExporter>();

			// Token: 0x04001F47 RID: 8007
			private readonly NativeStorePropertyDefinition prop;

			// Token: 0x04001F48 RID: 8008
			private readonly ContactValueType type;

			// Token: 0x04001F49 RID: 8009
			private readonly string propName;
		}

		// Token: 0x02000587 RID: 1415
		private class AddressExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A4C RID: 14924 RVA: 0x000EF73C File Offset: 0x000ED93C
			static AddressExporter()
			{
				OutboundVCardConverter.AddressExporter.instances.Add(new OutboundVCardConverter.AddressExporter(new NativeStorePropertyDefinition[]
				{
					InternalSchema.WorkPostOfficeBox,
					InternalSchema.OfficeLocation,
					InternalSchema.WorkAddressStreet,
					InternalSchema.WorkAddressCity,
					InternalSchema.WorkAddressState,
					InternalSchema.WorkAddressPostalCode,
					InternalSchema.WorkAddressCountry
				}, InternalSchema.BusinessAddress, PhysicalAddressType.Business, "WORK"));
				OutboundVCardConverter.AddressExporter.instances.Add(new OutboundVCardConverter.AddressExporter(new NativeStorePropertyDefinition[]
				{
					InternalSchema.HomePostOfficeBox,
					null,
					InternalSchema.HomeStreet,
					InternalSchema.HomeCity,
					InternalSchema.HomeState,
					InternalSchema.HomePostalCode,
					InternalSchema.HomeCountry
				}, InternalSchema.HomeAddress, PhysicalAddressType.Home, "HOME"));
				OutboundVCardConverter.AddressExporter.instances.Add(new OutboundVCardConverter.AddressExporter(new NativeStorePropertyDefinition[]
				{
					InternalSchema.OtherPostOfficeBox,
					null,
					InternalSchema.OtherStreet,
					InternalSchema.OtherCity,
					InternalSchema.OtherState,
					InternalSchema.OtherPostalCode,
					InternalSchema.OtherCountry
				}, InternalSchema.OtherAddress, PhysicalAddressType.Other, "POSTAL"));
			}

			// Token: 0x06003A4D RID: 14925 RVA: 0x000EF854 File Offset: 0x000EDA54
			public static void RegisterAll(List<OutboundVCardConverter.PropertyExporter> list)
			{
				foreach (OutboundVCardConverter.AddressExporter item in OutboundVCardConverter.AddressExporter.instances)
				{
					list.Add(item);
				}
			}

			// Token: 0x06003A4E RID: 14926 RVA: 0x000EF8A8 File Offset: 0x000EDAA8
			private AddressExporter(NativeStorePropertyDefinition[] props, PropertyDefinition labelProp, PhysicalAddressType addrType, string type)
			{
				this.props = props;
				this.labelProp = labelProp;
				this.addrType = addrType;
				this.type = type;
			}

			// Token: 0x06003A4F RID: 14927 RVA: 0x000EF8D0 File Offset: 0x000EDAD0
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				writer.StartProperty(PropertyId.Address);
				writer.StartParameter(ParameterId.Type);
				writer.WriteParameterValue(this.type);
				PhysicalAddressType valueOrDefault = contact.GetValueOrDefault<PhysicalAddressType>(ContactSchema.PostalAddressId);
				if (valueOrDefault == this.addrType)
				{
					writer.WriteParameterValue("PREF");
				}
				for (int i = 0; i < this.props.Length; i++)
				{
					string text = string.Empty;
					if (this.props[i] != null)
					{
						text = (contact.TryGetProperty(this.props[i]) as string);
						if (text == null)
						{
							text = string.Empty;
						}
					}
					writer.WritePropertyValue(text, ContactValueSeparators.Semicolon);
				}
				string text2 = contact.TryGetProperty(this.labelProp) as string;
				if (text2 != null)
				{
					writer.StartProperty(PropertyId.Label);
					writer.StartParameter(ParameterId.Type);
					writer.WriteParameterValue(this.type);
					if (valueOrDefault == this.addrType)
					{
						writer.WriteParameterValue("PREF");
					}
					writer.WritePropertyValue(text2);
				}
			}

			// Token: 0x04001F4A RID: 8010
			private static readonly List<OutboundVCardConverter.AddressExporter> instances = new List<OutboundVCardConverter.AddressExporter>();

			// Token: 0x04001F4B RID: 8011
			private readonly NativeStorePropertyDefinition[] props;

			// Token: 0x04001F4C RID: 8012
			private readonly PhysicalAddressType addrType;

			// Token: 0x04001F4D RID: 8013
			private readonly string type;

			// Token: 0x04001F4E RID: 8014
			private readonly PropertyDefinition labelProp;
		}

		// Token: 0x02000588 RID: 1416
		private class TypedPropertyStringExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A50 RID: 14928 RVA: 0x000EF9AC File Offset: 0x000EDBAC
			static TypedPropertyStringExporter()
			{
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.BusinessPhoneNumber, "TEL", new string[]
				{
					"WORK"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.BusinessPhoneNumber2, "TEL", new string[]
				{
					"WORK"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.HomePhone, "TEL", new string[]
				{
					"HOME"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.HomePhone2, "TEL", new string[]
				{
					"HOME"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.OtherFax, "TEL", new string[]
				{
					"FAX"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.MobilePhone, "TEL", new string[]
				{
					"CELL"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.Pager, "TEL", new string[]
				{
					"PAGER"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.CarPhone, "TEL", new string[]
				{
					"CAR"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.InternationalIsdnNumber, "TEL", new string[]
				{
					"ISDN"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.HomeFax, "TEL", new string[]
				{
					"HOME",
					"FAX"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.FaxNumber, "TEL", new string[]
				{
					"WORK",
					"FAX"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.PrimaryTelephoneNumber, "TEL", new string[]
				{
					"PREF"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.OtherTelephone, "TEL", new string[]
				{
					"VOICE"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.AssistantPhoneNumber, "X-MS-TEL", new string[]
				{
					"ASSISTANT"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.TtyTddPhoneNumber, "X-MS-TEL", new string[]
				{
					"TTYTDD"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.CallbackPhone, "X-MS-TEL", new string[]
				{
					"CALLBACK"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.RadioPhone, "X-MS-TEL", new string[]
				{
					"RADIO"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.OrganizationMainPhone, "X-MS-TEL", new string[]
				{
					"COMPANY"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.SpouseName, "X-MS-SPOUSE", new string[]
				{
					"N"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.Manager, "X-MS-MANAGER", new string[]
				{
					"N"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.AssistantName, "X-MS-ASSISTANT", new string[]
				{
					"N"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.TelexNumber, "EMAIL", new string[]
				{
					"TLX"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.Nickname, "NICKNAME", new string[0]));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.Title, "TITLE", new string[0]));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.Profession, "ROLE", new string[0]));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.IMAddress, "X-MS-IMADDRESS", new string[0]));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.IMAddress2, "X-MS-IMADDRESS", new string[0]));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.IMAddress3, "X-MS-IMADDRESS", new string[0]));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.FreeBusyUrl, "FBURL", new string[0]));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.Hobbies, "X-MS-INTERESTS", new string[0]));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.PersonalHomePage, "URL", new string[]
				{
					"HOME"
				}));
				OutboundVCardConverter.TypedPropertyStringExporter.instances.Add(new OutboundVCardConverter.TypedPropertyStringExporter(InternalSchema.BusinessHomePage, "URL", new string[]
				{
					"WORK"
				}));
			}

			// Token: 0x06003A51 RID: 14929 RVA: 0x000EFEE4 File Offset: 0x000EE0E4
			public static void RegisterAll(List<OutboundVCardConverter.PropertyExporter> list)
			{
				foreach (OutboundVCardConverter.TypedPropertyStringExporter item in OutboundVCardConverter.TypedPropertyStringExporter.instances)
				{
					list.Add(item);
				}
			}

			// Token: 0x06003A52 RID: 14930 RVA: 0x000EFF38 File Offset: 0x000EE138
			private TypedPropertyStringExporter(NativeStorePropertyDefinition prop, string propName, params string[] types)
			{
				this.prop = prop;
				this.types = types;
				this.propName = propName;
			}

			// Token: 0x06003A53 RID: 14931 RVA: 0x000EFF58 File Offset: 0x000EE158
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				string text = contact.TryGetProperty(this.prop) as string;
				if (text != null)
				{
					writer.StartProperty(this.propName);
					if (this.types != null && this.types.Length > 0)
					{
						writer.StartParameter(ParameterId.Type);
						foreach (string value in this.types)
						{
							writer.WriteParameterValue(value);
						}
					}
					writer.WritePropertyValue(text);
				}
			}

			// Token: 0x04001F4F RID: 8015
			private static readonly List<OutboundVCardConverter.TypedPropertyStringExporter> instances = new List<OutboundVCardConverter.TypedPropertyStringExporter>();

			// Token: 0x04001F50 RID: 8016
			private readonly NativeStorePropertyDefinition prop;

			// Token: 0x04001F51 RID: 8017
			private readonly string propName;

			// Token: 0x04001F52 RID: 8018
			private readonly string[] types;
		}

		// Token: 0x02000589 RID: 1417
		private class OrgExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A54 RID: 14932 RVA: 0x000EFFC7 File Offset: 0x000EE1C7
			private OrgExporter()
			{
			}

			// Token: 0x06003A55 RID: 14933 RVA: 0x000EFFD0 File Offset: 0x000EE1D0
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				writer.StartProperty(PropertyId.Organization);
				string valueOrDefault = contact.GetValueOrDefault<string>(InternalSchema.CompanyName, string.Empty);
				writer.WritePropertyValue(valueOrDefault, ContactValueSeparators.Semicolon);
				valueOrDefault = contact.GetValueOrDefault<string>(InternalSchema.Department, string.Empty);
				writer.WritePropertyValue(valueOrDefault, ContactValueSeparators.Semicolon);
			}

			// Token: 0x04001F53 RID: 8019
			public static readonly OutboundVCardConverter.OrgExporter Instance = new OutboundVCardConverter.OrgExporter();
		}

		// Token: 0x0200058A RID: 1418
		private class NoteExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A57 RID: 14935 RVA: 0x000F0023 File Offset: 0x000EE223
			private NoteExporter()
			{
			}

			// Token: 0x06003A58 RID: 14936 RVA: 0x000F002C File Offset: 0x000EE22C
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				if (contact.Body.IsBodyDefined)
				{
					BodyReadConfiguration configuration = new BodyReadConfiguration(BodyFormat.TextPlain, context.Encoding.WebName);
					using (Stream stream = contact.Body.OpenReadStream(configuration))
					{
						writer.StartProperty(PropertyId.Note);
						writer.WritePropertyValue(stream);
					}
				}
			}

			// Token: 0x04001F54 RID: 8020
			public static readonly OutboundVCardConverter.NoteExporter Instance = new OutboundVCardConverter.NoteExporter();
		}

		// Token: 0x0200058B RID: 1419
		private class ClassExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A5A RID: 14938 RVA: 0x000F009C File Offset: 0x000EE29C
			private ClassExporter()
			{
			}

			// Token: 0x06003A5B RID: 14939 RVA: 0x000F00A4 File Offset: 0x000EE2A4
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				Sensitivity? sensitivity = contact.TryGetProperty(InternalSchema.Sensitivity) as Sensitivity?;
				if (sensitivity != null)
				{
					switch (sensitivity.Value)
					{
					case Sensitivity.Normal:
						writer.WriteProperty(PropertyId.Class, "PUBLIC");
						break;
					case Sensitivity.Personal:
					case Sensitivity.Private:
						writer.WriteProperty(PropertyId.Class, "PRIVATE");
						return;
					case Sensitivity.CompanyConfidential:
						writer.WriteProperty(PropertyId.Class, "CONFIDENTIAL");
						return;
					default:
						return;
					}
				}
			}

			// Token: 0x04001F55 RID: 8021
			public static readonly OutboundVCardConverter.ClassExporter Instance = new OutboundVCardConverter.ClassExporter();
		}

		// Token: 0x0200058C RID: 1420
		private class KeyExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A5D RID: 14941 RVA: 0x000F0124 File Offset: 0x000EE324
			private KeyExporter()
			{
			}

			// Token: 0x06003A5E RID: 14942 RVA: 0x000F012C File Offset: 0x000EE32C
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				byte[][] array = contact.TryGetProperty(InternalSchema.UserX509Certificates) as byte[][];
				if (array != null)
				{
					foreach (byte[] array3 in array)
					{
						if (array3 != null)
						{
							writer.StartProperty(PropertyId.Key);
							writer.WriteParameter(ParameterId.Type, "X509");
							writer.WriteParameter(ParameterId.Encoding, "B");
							using (MemoryStream memoryStream = new MemoryStream())
							{
								using (Stream stream = new EncoderStream(new StreamWrapper(memoryStream, false), new Base64Encoder(0), EncoderStreamAccess.Write))
								{
									stream.Write(array3, 0, array3.Length);
								}
								memoryStream.Position = 0L;
								writer.WritePropertyValue(memoryStream);
							}
						}
					}
				}
			}

			// Token: 0x04001F56 RID: 8022
			public static readonly OutboundVCardConverter.KeyExporter Instance = new OutboundVCardConverter.KeyExporter();
		}

		// Token: 0x0200058D RID: 1421
		private class StreamTextExporter : OutboundVCardConverter.PropertyExporter
		{
			// Token: 0x06003A60 RID: 14944 RVA: 0x000F0208 File Offset: 0x000EE408
			static StreamTextExporter()
			{
				OutboundVCardConverter.StreamTextExporter.Instances.Add(new OutboundVCardConverter.StreamTextExporter(InternalSchema.UserText1, "X-MS-TEXT"));
				OutboundVCardConverter.StreamTextExporter.Instances.Add(new OutboundVCardConverter.StreamTextExporter(InternalSchema.UserText2, "X-MS-TEXT"));
				OutboundVCardConverter.StreamTextExporter.Instances.Add(new OutboundVCardConverter.StreamTextExporter(InternalSchema.UserText3, "X-MS-TEXT"));
				OutboundVCardConverter.StreamTextExporter.Instances.Add(new OutboundVCardConverter.StreamTextExporter(InternalSchema.UserText4, "X-MS-TEXT"));
			}

			// Token: 0x06003A61 RID: 14945 RVA: 0x000F0284 File Offset: 0x000EE484
			public static void RegisterAll(List<OutboundVCardConverter.PropertyExporter> list)
			{
				foreach (OutboundVCardConverter.StreamTextExporter item in OutboundVCardConverter.StreamTextExporter.Instances)
				{
					list.Add(item);
				}
			}

			// Token: 0x06003A62 RID: 14946 RVA: 0x000F02D8 File Offset: 0x000EE4D8
			private StreamTextExporter(NativeStorePropertyDefinition prop, string propName)
			{
				this.prop = prop;
				this.propName = propName;
			}

			// Token: 0x06003A63 RID: 14947 RVA: 0x000F02F0 File Offset: 0x000EE4F0
			public override void Export(ContactWriter writer, Contact contact, OutboundVCardConverter.PropertyExporter.Context context)
			{
				object obj = contact.TryGetProperty(this.prop);
				if (obj is string)
				{
					writer.WriteProperty(this.propName, obj as string);
					return;
				}
				if (PropertyError.IsPropertyValueTooBig(obj))
				{
					writer.StartProperty(this.propName);
					using (Stream stream = contact.OpenPropertyStream(this.prop, PropertyOpenMode.ReadOnly))
					{
						using (Stream stream2 = new ConverterStream(stream, new TextToText
						{
							InputEncoding = Encoding.Unicode,
							OutputEncoding = context.Encoding
						}, ConverterStreamAccess.Read))
						{
							writer.WritePropertyValue(stream2);
						}
					}
				}
			}

			// Token: 0x04001F57 RID: 8023
			public static readonly List<OutboundVCardConverter.StreamTextExporter> Instances = new List<OutboundVCardConverter.StreamTextExporter>();

			// Token: 0x04001F58 RID: 8024
			private readonly NativeStorePropertyDefinition prop;

			// Token: 0x04001F59 RID: 8025
			private readonly string propName;
		}
	}
}
