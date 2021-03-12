using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.ContentTypes.vCard;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime.Encoders;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200055E RID: 1374
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class InboundVCardConverter
	{
		// Token: 0x060039E7 RID: 14823 RVA: 0x000ED460 File Offset: 0x000EB660
		static InboundVCardConverter()
		{
			InboundVCardConverter.rootHandlersMap.Add("FN", new InboundVCardConverter.FormattedNamePropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("N", new InboundVCardConverter.NamePropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("NICKNAME", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.Nickname));
			InboundVCardConverter.rootHandlersMap.Add("PHOTO", new InboundVCardConverter.PhotoPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("BDAY", new InboundVCardConverter.DatePropertyHandler(ContactSchema.Birthday));
			InboundVCardConverter.rootHandlersMap.Add("ADR", new InboundVCardConverter.AddressPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("TEL", new InboundVCardConverter.PhonePropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("EMAIL", new InboundVCardConverter.EmailPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("TITLE", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.Title));
			InboundVCardConverter.rootHandlersMap.Add("ROLE", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.Profession));
			InboundVCardConverter.rootHandlersMap.Add("AGENT", new InboundVCardConverter.AgentPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("ORG", new InboundVCardConverter.OrgPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("CATEGORIES", new InboundVCardConverter.MultiStringPropertyHandler(InternalSchema.Categories));
			InboundVCardConverter.rootHandlersMap.Add("NOTE", new InboundVCardConverter.NotePropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("REV", new InboundVCardConverter.DateTimePropertyHandler(StoreObjectSchema.LastModifiedTime));
			InboundVCardConverter.rootHandlersMap.Add("URL", new InboundVCardConverter.UrlPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("CLASS", new InboundVCardConverter.SensitivityPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("X-MS-OL-DESIGN", new InboundVCardConverter.OutlookDesignPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("X-MS-CHILD", new InboundVCardConverter.MultiStringPropertyHandler(InternalSchema.Children));
			InboundVCardConverter.rootHandlersMap.Add("X-CHILD", new InboundVCardConverter.MultiStringPropertyHandler(InternalSchema.Children));
			InboundVCardConverter.rootHandlersMap.Add("X-MS-TEXT", new InboundVCardConverter.MsTextPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("X-CUSTOM", new InboundVCardConverter.MsTextPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("X-MS-IMADDRESS", new InboundVCardConverter.ImAddressPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("X-MS-RM-IMACCOUNT", new InboundVCardConverter.ImAddressPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("X-MS-TEL", new InboundVCardConverter.MsTelPropertyHandler());
			InboundVCardConverter.rootHandlersMap.Add("X-MS-RM-COMPANYTELEPHONE", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.OrganizationMainPhone));
			InboundVCardConverter.rootHandlersMap.Add("X-MS-ANNIVERSARY", new InboundVCardConverter.DatePropertyHandler(ContactSchema.WeddingAnniversary));
			InboundVCardConverter.rootHandlersMap.Add("X-ANNIVERSARY", new InboundVCardConverter.DatePropertyHandler(ContactSchema.WeddingAnniversary));
			InboundVCardConverter.rootHandlersMap.Add("X-MS-SPOUSE", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.SpouseName));
			InboundVCardConverter.rootHandlersMap.Add("X-MS-MANAGER", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.Manager));
			InboundVCardConverter.rootHandlersMap.Add("X-MS-ASSISTANT", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.AssistantName));
			InboundVCardConverter.rootHandlersMap.Add("X-ASSISTANT", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.AssistantName));
			InboundVCardConverter.rootHandlersMap.Add("FBURL", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.FreeBusyUrl));
			InboundVCardConverter.rootHandlersMap.Add("X-MS-INTERESTS", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.Hobbies));
			InboundVCardConverter.rootHandlersMap.Add("X-INTERESTS", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.Hobbies));
			InboundVCardConverter.agentHandlersMap = new Dictionary<string, InboundVCardConverter.PropertyHandler>(StringComparer.OrdinalIgnoreCase);
			InboundVCardConverter.agentHandlersMap.Add("FN", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.AssistantName));
			InboundVCardConverter.agentHandlersMap.Add("TEL", new InboundVCardConverter.SimpleTextPropertyHandler(ContactSchema.AssistantPhoneNumber));
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000ED7D3 File Offset: 0x000EB9D3
		internal static void Convert(Stream dataStream, Encoding encoding, Contact contact, InboundConversionOptions options)
		{
			InboundVCardConverter.Convert(dataStream, encoding, contact, options, InboundVCardConverter.rootHandlersMap);
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000ED7E4 File Offset: 0x000EB9E4
		private static void Convert(Stream dataStream, Encoding encoding, Contact contact, InboundConversionOptions options, Dictionary<string, InboundVCardConverter.PropertyHandler> handlersMap)
		{
			Util.ThrowOnNullArgument(options, "options");
			if (!options.IgnoreImceaDomain)
			{
				Util.ThrowOnNullOrEmptyArgument(options.ImceaEncapsulationDomain, "options.ImceaEncapsulationDomain");
			}
			contact[InternalSchema.ItemClass] = "IPM.Contact";
			contact[InternalSchema.PostalAddressId] = PhysicalAddressType.None;
			contact[InternalSchema.ConversationIndex] = ConversationIndex.CreateNew().ToByteArray();
			contact.FileAs = FileAsMapping.LastCommaFirst;
			ContactReader contactReader = new ContactReader(dataStream, encoding, ContactComplianceMode.Loose);
			try
			{
				if (!contactReader.ReadNext())
				{
					StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundVCardTracer, "InboundVCardConverter::Convert - vCard not found");
					throw new ConversionFailedException(ConversionFailureReason.CorruptContent);
				}
				ContactPropertyReader propertyReader = contactReader.PropertyReader;
				InboundVCardConverter.ProcessingContext context = new InboundVCardConverter.ProcessingContext(contact, handlersMap, encoding, options);
				while (propertyReader.ReadNextProperty())
				{
					InboundVCardConverter.ProcessProperty(propertyReader, context);
				}
			}
			catch (ExchangeDataException ex)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcInboundVCardTracer, ex.ToString());
				throw new ConversionFailedException(ConversionFailureReason.CorruptContent, ex);
			}
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000ED8D4 File Offset: 0x000EBAD4
		private static void ProcessProperty(ContactPropertyReader propertyReader, InboundVCardConverter.ProcessingContext context)
		{
			InboundVCardConverter.PropertyHandler propertyHandler = null;
			string text = propertyReader.Name;
			int num = text.IndexOf('.');
			if (num > 0 && num < text.Length - 1)
			{
				text = text.Substring(num + 1);
			}
			if (!context.HandlersMap.TryGetValue(text, out propertyHandler))
			{
				return;
			}
			propertyHandler.ProcessPropertyValue(context, propertyReader);
			context.UnnamedParameterValues.Clear();
			context.ApplicableTypes.Clear();
			context.Decoder = null;
			context.OverrideEncoding = null;
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000ED94C File Offset: 0x000EBB4C
		private static Encoding GetEncoding(string charset)
		{
			Encoding result = null;
			Charset.TryGetEncoding(charset, out result);
			return result;
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x000ED965 File Offset: 0x000EBB65
		private static ByteEncoder GetDecoder(string value)
		{
			if (string.Equals(value, "B", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "BASE64", StringComparison.OrdinalIgnoreCase))
			{
				return new Base64Decoder();
			}
			if (string.Equals(value, "QUOTED-PRINTABLE", StringComparison.OrdinalIgnoreCase))
			{
				return new QPDecoder();
			}
			return null;
		}

		// Token: 0x04001EF0 RID: 7920
		private const int MaxPropertySize = 32768;

		// Token: 0x04001EF1 RID: 7921
		private static Dictionary<string, InboundVCardConverter.PropertyHandler> rootHandlersMap = new Dictionary<string, InboundVCardConverter.PropertyHandler>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001EF2 RID: 7922
		private static Dictionary<string, InboundVCardConverter.PropertyHandler> agentHandlersMap;

		// Token: 0x0200055F RID: 1375
		private class ProcessingContext
		{
			// Token: 0x060039ED RID: 14829 RVA: 0x000ED99E File Offset: 0x000EBB9E
			public ProcessingContext(Contact contact, Dictionary<string, InboundVCardConverter.PropertyHandler> handlersMap, Encoding encoding, InboundConversionOptions options)
			{
				this.Contact = contact;
				this.HandlersMap = handlersMap;
				this.Encoding = encoding;
				this.Options = options;
			}

			// Token: 0x04001EF3 RID: 7923
			public Dictionary<string, InboundVCardConverter.PropertyHandler> HandlersMap;

			// Token: 0x04001EF4 RID: 7924
			public Contact Contact;

			// Token: 0x04001EF5 RID: 7925
			public List<string> UnnamedParameterValues = new List<string>();

			// Token: 0x04001EF6 RID: 7926
			public List<string> ApplicableTypes = new List<string>();

			// Token: 0x04001EF7 RID: 7927
			public ByteEncoder Decoder;

			// Token: 0x04001EF8 RID: 7928
			public Encoding Encoding;

			// Token: 0x04001EF9 RID: 7929
			public Encoding OverrideEncoding;

			// Token: 0x04001EFA RID: 7930
			public InboundConversionOptions Options;
		}

		// Token: 0x02000560 RID: 1376
		private abstract class PropertyHandler
		{
			// Token: 0x170011FA RID: 4602
			// (get) Token: 0x060039EE RID: 14830 RVA: 0x000ED9D9 File Offset: 0x000EBBD9
			public InboundVCardConverter.PropertyHandler.HandlerOptions Options
			{
				get
				{
					return this.options;
				}
			}

			// Token: 0x060039EF RID: 14831 RVA: 0x000ED9E1 File Offset: 0x000EBBE1
			protected PropertyHandler(InboundVCardConverter.PropertyHandler.HandlerOptions options)
			{
				this.options = options;
			}

			// Token: 0x060039F0 RID: 14832 RVA: 0x000ED9F0 File Offset: 0x000EBBF0
			protected void ProcessParameters(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				ContactParameterReader parameterReader = reader.ParameterReader;
				while (parameterReader.ReadNextParameter())
				{
					if (parameterReader.Name == null)
					{
						context.UnnamedParameterValues.Add(parameterReader.ReadValue());
					}
					else
					{
						switch (parameterReader.ParameterId)
						{
						case ParameterId.Type:
							while (parameterReader.ReadNextValue())
							{
								context.ApplicableTypes.Add(parameterReader.ReadValue());
							}
							break;
						case ParameterId.Encoding:
							if ((this.options & InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder) == InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder)
							{
								context.Decoder = InboundVCardConverter.GetDecoder(parameterReader.ReadValue());
							}
							break;
						case ParameterId.Charset:
							if ((this.options & InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset) == InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
							{
								context.OverrideEncoding = InboundVCardConverter.GetEncoding(parameterReader.ReadValue());
							}
							break;
						}
					}
				}
				if (this.options == InboundVCardConverter.PropertyHandler.HandlerOptions.None)
				{
					return;
				}
				List<string> list = new List<string>();
				foreach (string text in context.UnnamedParameterValues)
				{
					if ((this.options & InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder) == InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder && context.Decoder == null)
					{
						context.Decoder = InboundVCardConverter.GetDecoder(text);
						if (context.Decoder != null)
						{
							continue;
						}
					}
					if ((this.options & InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset) == InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset && context.OverrideEncoding == null)
					{
						context.OverrideEncoding = InboundVCardConverter.GetEncoding(text);
						if (context.OverrideEncoding != null)
						{
							continue;
						}
					}
					list.Add(text);
				}
				context.UnnamedParameterValues = list;
				if ((this.options & InboundVCardConverter.PropertyHandler.HandlerOptions.MustHaveDecoder) == InboundVCardConverter.PropertyHandler.HandlerOptions.MustHaveDecoder && context.Decoder == null)
				{
					context.Decoder = new Base64Decoder();
				}
				if (context.Decoder != null || context.OverrideEncoding != null)
				{
					reader.ApplyValueOverrides(context.OverrideEncoding, context.Decoder);
				}
			}

			// Token: 0x060039F1 RID: 14833 RVA: 0x000EDBA4 File Offset: 0x000EBDA4
			public void ProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				this.ProcessParameters(context, reader);
				this.InternalProcessPropertyValue(context, reader);
			}

			// Token: 0x060039F2 RID: 14834
			protected abstract void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader);

			// Token: 0x04001EFB RID: 7931
			private InboundVCardConverter.PropertyHandler.HandlerOptions options;

			// Token: 0x02000561 RID: 1377
			[Flags]
			public enum HandlerOptions
			{
				// Token: 0x04001EFD RID: 7933
				CanHaveDecoder = 1,
				// Token: 0x04001EFE RID: 7934
				CanOverrideCharset = 2,
				// Token: 0x04001EFF RID: 7935
				MustHaveDecoder = 5,
				// Token: 0x04001F00 RID: 7936
				None = 0
			}
		}

		// Token: 0x02000562 RID: 1378
		private abstract class StructuredTextHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x060039F3 RID: 14835 RVA: 0x000EDBB6 File Offset: 0x000EBDB6
			protected StructuredTextHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder | InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
			{
			}

			// Token: 0x060039F4 RID: 14836 RVA: 0x000EDBC0 File Offset: 0x000EBDC0
			protected static string[] ReadStructuredText(ContactPropertyReader reader, int expectedCount, InboundVCardConverter.ProcessingContext context)
			{
				string[] array = new string[expectedCount];
				for (int i = 0; i < array.Length; i++)
				{
					if (!reader.ReadNextValue())
					{
						while (i < array.Length)
						{
							array[i] = string.Empty;
							i++;
						}
						break;
					}
					array[i] = reader.ReadValueAsString(ContactValueSeparators.Semicolon).Trim();
				}
				return array;
			}
		}

		// Token: 0x02000563 RID: 1379
		private abstract class MultiPropertyTextHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x060039F5 RID: 14837 RVA: 0x000EDC12 File Offset: 0x000EBE12
			protected MultiPropertyTextHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder | InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
			{
			}

			// Token: 0x060039F6 RID: 14838 RVA: 0x000EDC1C File Offset: 0x000EBE1C
			protected static void SetArrayPropertyValue(string value, Contact contact, params PropertyDefinition[] propertyList)
			{
				for (int i = 0; i < propertyList.Length; i++)
				{
					if (contact.TryGetProperty(propertyList[i]) is PropertyError)
					{
						contact[propertyList[i]] = value;
						return;
					}
				}
			}
		}

		// Token: 0x02000564 RID: 1380
		private class SimpleTextPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x060039F7 RID: 14839 RVA: 0x000EDC52 File Offset: 0x000EBE52
			public SimpleTextPropertyHandler(PropertyDefinition prop) : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder | InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
			{
				this.prop = prop;
			}

			// Token: 0x060039F8 RID: 14840 RVA: 0x000EDC62 File Offset: 0x000EBE62
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				context.Contact[this.prop] = reader.ReadValueAsString();
			}

			// Token: 0x04001F01 RID: 7937
			private readonly PropertyDefinition prop;
		}

		// Token: 0x02000565 RID: 1381
		private class DatePropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x060039F9 RID: 14841 RVA: 0x000EDC7C File Offset: 0x000EBE7C
			public DatePropertyHandler(PropertyDefinition prop) : base(InboundVCardConverter.PropertyHandler.HandlerOptions.None)
			{
				this.prop = prop;
			}

			// Token: 0x060039FA RID: 14842 RVA: 0x000EDC8C File Offset: 0x000EBE8C
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				ExTimeZone desiredTimeZone = ExTimeZone.CurrentTimeZone;
				DateTime value = reader.ReadValueAsDateTime(ContactValueType.Date);
				if (context.Contact.Session != null)
				{
					desiredTimeZone = context.Contact.Session.ExTimeZone;
				}
				context.Contact[this.prop] = new ExDateTime(desiredTimeZone, DateTime.SpecifyKind(value, DateTimeKind.Local).Date);
			}

			// Token: 0x04001F02 RID: 7938
			private readonly PropertyDefinition prop;
		}

		// Token: 0x02000566 RID: 1382
		private class DateTimePropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x060039FB RID: 14843 RVA: 0x000EDCF1 File Offset: 0x000EBEF1
			public DateTimePropertyHandler(PropertyDefinition prop) : base(InboundVCardConverter.PropertyHandler.HandlerOptions.None)
			{
				this.prop = prop;
			}

			// Token: 0x060039FC RID: 14844 RVA: 0x000EDD04 File Offset: 0x000EBF04
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				DateTime dateTime = reader.ReadValueAsDateTime(ContactValueType.Date);
				ExDateTime exDateTime = new ExDateTime(ExTimeZone.UtcTimeZone, dateTime);
				context.Contact[this.prop] = exDateTime;
			}

			// Token: 0x04001F03 RID: 7939
			private readonly PropertyDefinition prop;
		}

		// Token: 0x02000567 RID: 1383
		private abstract class TextStreamPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x060039FD RID: 14845 RVA: 0x000EDD3E File Offset: 0x000EBF3E
			protected TextStreamPropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder | InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
			{
			}

			// Token: 0x060039FE RID: 14846 RVA: 0x000EDD48 File Offset: 0x000EBF48
			protected static Stream GetUnicodeReadStream(ContactPropertyReader reader, InboundVCardConverter.ProcessingContext context)
			{
				Stream valueReadStream = reader.GetValueReadStream();
				return new ConverterStream(valueReadStream, new TextToText
				{
					InputEncoding = (context.OverrideEncoding ?? context.Encoding),
					OutputEncoding = Encoding.Unicode
				}, ConverterStreamAccess.Read);
			}
		}

		// Token: 0x02000568 RID: 1384
		private class FormattedNamePropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x060039FF RID: 14847 RVA: 0x000EDD8E File Offset: 0x000EBF8E
			public FormattedNamePropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder | InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
			{
			}

			// Token: 0x06003A00 RID: 14848 RVA: 0x000EDD98 File Offset: 0x000EBF98
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				string value = reader.ReadValueAsString();
				context.Contact[StoreObjectSchema.DisplayName] = value;
				context.Contact[ItemSchema.NormalizedSubject] = value;
				context.Contact[InternalSchema.ConversationTopic] = value;
			}
		}

		// Token: 0x02000569 RID: 1385
		private class NamePropertyHandler : InboundVCardConverter.StructuredTextHandler
		{
			// Token: 0x06003A01 RID: 14849 RVA: 0x000EDDE0 File Offset: 0x000EBFE0
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				string[] array = InboundVCardConverter.StructuredTextHandler.ReadStructuredText(reader, 5, context);
				context.Contact[ContactSchema.Surname] = array[0];
				context.Contact[ContactSchema.GivenName] = array[1];
				context.Contact[ContactSchema.MiddleName] = array[2];
				context.Contact[ContactSchema.DisplayNamePrefix] = array[3];
				context.Contact[ContactSchema.Generation] = array[4];
			}
		}

		// Token: 0x0200056A RID: 1386
		private class PhotoPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x06003A03 RID: 14851 RVA: 0x000EDE60 File Offset: 0x000EC060
			static PhotoPropertyHandler()
			{
				InboundVCardConverter.PhotoPropertyHandler.typeToFilenameMap.Add("GIF", "ContactPicture.gif");
				InboundVCardConverter.PhotoPropertyHandler.typeToFilenameMap.Add("JPEG", "ContactPicture.jpg");
				InboundVCardConverter.PhotoPropertyHandler.typeToFilenameMap.Add("BMP", "ContactPicture.bmp");
				InboundVCardConverter.PhotoPropertyHandler.typeToFilenameMap.Add("PNG", "ContactPicture.png");
			}

			// Token: 0x06003A04 RID: 14852 RVA: 0x000EDECC File Offset: 0x000EC0CC
			public PhotoPropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.MustHaveDecoder)
			{
			}

			// Token: 0x06003A05 RID: 14853 RVA: 0x000EDED8 File Offset: 0x000EC0D8
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				if (reader.ValueType != ContactValueType.Binary)
				{
					return;
				}
				string text = null;
				foreach (string key in context.ApplicableTypes)
				{
					if (InboundVCardConverter.PhotoPropertyHandler.typeToFilenameMap.TryGetValue(key, out text))
					{
						break;
					}
				}
				if (text == null)
				{
					foreach (string key2 in context.UnnamedParameterValues)
					{
						if (InboundVCardConverter.PhotoPropertyHandler.typeToFilenameMap.TryGetValue(key2, out text))
						{
							break;
						}
					}
					if (text == null)
					{
						return;
					}
				}
				using (Stream valueReadStream = reader.GetValueReadStream())
				{
					using (StreamAttachment streamAttachment = context.Contact.AttachmentCollection.Create(AttachmentType.Stream) as StreamAttachment)
					{
						streamAttachment[AttachmentSchema.IsContactPhoto] = true;
						streamAttachment.FileName = text;
						using (Stream contentStream = streamAttachment.GetContentStream(PropertyOpenMode.Create))
						{
							Util.StreamHandler.CopyStreamData(valueReadStream, contentStream);
						}
						streamAttachment.Save();
					}
				}
			}

			// Token: 0x04001F04 RID: 7940
			private static Dictionary<string, string> typeToFilenameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0200056B RID: 1387
		private class AddressPropertyHandler : InboundVCardConverter.StructuredTextHandler
		{
			// Token: 0x06003A06 RID: 14854 RVA: 0x000EE034 File Offset: 0x000EC234
			static AddressPropertyHandler()
			{
				InboundVCardConverter.AddressPropertyHandler.addressTypeMap.Add("DOM", InboundVCardConverter.AddressPropertyHandler.AddressTypes.Domestic);
				InboundVCardConverter.AddressPropertyHandler.addressTypeMap.Add("INTL", InboundVCardConverter.AddressPropertyHandler.AddressTypes.International);
				InboundVCardConverter.AddressPropertyHandler.addressTypeMap.Add("PREF", InboundVCardConverter.AddressPropertyHandler.AddressTypes.Preferred);
				InboundVCardConverter.AddressPropertyHandler.addressTypeMap.Add("WORK", InboundVCardConverter.AddressPropertyHandler.AddressTypes.Work);
				InboundVCardConverter.AddressPropertyHandler.addressTypeMap.Add("HOME", InboundVCardConverter.AddressPropertyHandler.AddressTypes.Home);
				InboundVCardConverter.AddressPropertyHandler.addressTypeMap.Add("POSTAL", InboundVCardConverter.AddressPropertyHandler.AddressTypes.Postal);
				InboundVCardConverter.AddressPropertyHandler.addressTypeMap.Add("PARCEL", InboundVCardConverter.AddressPropertyHandler.AddressTypes.Parcel);
			}

			// Token: 0x06003A07 RID: 14855 RVA: 0x000EE0C3 File Offset: 0x000EC2C3
			private static string CombineStreetAddress(string value1, string value2)
			{
				if (!string.IsNullOrEmpty(value1) && !string.IsNullOrEmpty(value2))
				{
					return value1 + "\n" + value2;
				}
				if (!string.IsNullOrEmpty(value1))
				{
					return value1;
				}
				return value2;
			}

			// Token: 0x06003A08 RID: 14856 RVA: 0x000EE0F0 File Offset: 0x000EC2F0
			private static InboundVCardConverter.AddressPropertyHandler.AddressTypes GetAddressType(List<string> values)
			{
				InboundVCardConverter.AddressPropertyHandler.AddressTypes addressTypes = InboundVCardConverter.AddressPropertyHandler.AddressTypes.None;
				foreach (string key in values)
				{
					InboundVCardConverter.AddressPropertyHandler.AddressTypes addressTypes2 = InboundVCardConverter.AddressPropertyHandler.AddressTypes.None;
					if (InboundVCardConverter.AddressPropertyHandler.addressTypeMap.TryGetValue(key, out addressTypes2))
					{
						addressTypes |= addressTypes2;
					}
				}
				return addressTypes;
			}

			// Token: 0x06003A09 RID: 14857 RVA: 0x000EE150 File Offset: 0x000EC350
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				InboundVCardConverter.AddressPropertyHandler.AddressTypes addressTypes = InboundVCardConverter.AddressPropertyHandler.GetAddressType(context.ApplicableTypes) | InboundVCardConverter.AddressPropertyHandler.GetAddressType(context.UnnamedParameterValues);
				if (addressTypes == InboundVCardConverter.AddressPropertyHandler.AddressTypes.None)
				{
					addressTypes = InboundVCardConverter.AddressPropertyHandler.AddressTypes.Default;
				}
				string[] array = InboundVCardConverter.StructuredTextHandler.ReadStructuredText(reader, 7, context);
				if ((addressTypes & InboundVCardConverter.AddressPropertyHandler.AddressTypes.Work) != InboundVCardConverter.AddressPropertyHandler.AddressTypes.None)
				{
					context.Contact[ContactSchema.WorkPostOfficeBox] = array[0];
					context.Contact[ContactSchema.OfficeLocation] = array[1];
					context.Contact[ContactSchema.WorkAddressStreet] = array[2];
					context.Contact[ContactSchema.WorkAddressCity] = array[3];
					context.Contact[ContactSchema.WorkAddressState] = array[4];
					context.Contact[ContactSchema.WorkAddressPostalCode] = array[5];
					context.Contact[ContactSchema.WorkAddressCountry] = array[6];
					if ((addressTypes & InboundVCardConverter.AddressPropertyHandler.AddressTypes.Preferred) != InboundVCardConverter.AddressPropertyHandler.AddressTypes.None)
					{
						context.Contact[ContactSchema.PostalAddressId] = PhysicalAddressType.Business;
					}
				}
				if ((addressTypes & InboundVCardConverter.AddressPropertyHandler.AddressTypes.Home) != InboundVCardConverter.AddressPropertyHandler.AddressTypes.None)
				{
					context.Contact[ContactSchema.HomePostOfficeBox] = array[0];
					context.Contact[ContactSchema.HomeStreet] = InboundVCardConverter.AddressPropertyHandler.CombineStreetAddress(array[1], array[2]);
					context.Contact[ContactSchema.HomeCity] = array[3];
					context.Contact[ContactSchema.HomeState] = array[4];
					context.Contact[ContactSchema.HomePostalCode] = array[5];
					context.Contact[ContactSchema.HomeCountry] = array[6];
					if ((addressTypes & InboundVCardConverter.AddressPropertyHandler.AddressTypes.Preferred) != InboundVCardConverter.AddressPropertyHandler.AddressTypes.None)
					{
						context.Contact[ContactSchema.PostalAddressId] = PhysicalAddressType.Home;
					}
				}
				if ((addressTypes & (InboundVCardConverter.AddressPropertyHandler.AddressTypes.Home | InboundVCardConverter.AddressPropertyHandler.AddressTypes.Work)) == InboundVCardConverter.AddressPropertyHandler.AddressTypes.None)
				{
					context.Contact[ContactSchema.OtherPostOfficeBox] = array[0];
					context.Contact[ContactSchema.OtherStreet] = InboundVCardConverter.AddressPropertyHandler.CombineStreetAddress(array[1], array[2]);
					context.Contact[ContactSchema.OtherCity] = array[3];
					context.Contact[ContactSchema.OtherState] = array[4];
					context.Contact[ContactSchema.OtherPostalCode] = array[5];
					context.Contact[ContactSchema.OtherCountry] = array[6];
					if ((addressTypes & InboundVCardConverter.AddressPropertyHandler.AddressTypes.Preferred) != InboundVCardConverter.AddressPropertyHandler.AddressTypes.None)
					{
						context.Contact[ContactSchema.PostalAddressId] = PhysicalAddressType.Other;
					}
				}
			}

			// Token: 0x04001F05 RID: 7941
			private static Dictionary<string, InboundVCardConverter.AddressPropertyHandler.AddressTypes> addressTypeMap = new Dictionary<string, InboundVCardConverter.AddressPropertyHandler.AddressTypes>(StringComparer.OrdinalIgnoreCase);

			// Token: 0x0200056C RID: 1388
			[Flags]
			private enum AddressTypes
			{
				// Token: 0x04001F07 RID: 7943
				None = 0,
				// Token: 0x04001F08 RID: 7944
				Domestic = 1,
				// Token: 0x04001F09 RID: 7945
				International = 2,
				// Token: 0x04001F0A RID: 7946
				Home = 4,
				// Token: 0x04001F0B RID: 7947
				Work = 8,
				// Token: 0x04001F0C RID: 7948
				Preferred = 16,
				// Token: 0x04001F0D RID: 7949
				Postal = 32,
				// Token: 0x04001F0E RID: 7950
				Parcel = 64,
				// Token: 0x04001F0F RID: 7951
				Default = 106
			}
		}

		// Token: 0x0200056D RID: 1389
		private class PhonePropertyHandler : InboundVCardConverter.MultiPropertyTextHandler
		{
			// Token: 0x06003A0B RID: 14859 RVA: 0x000EE374 File Offset: 0x000EC574
			static PhonePropertyHandler()
			{
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("HOME", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Home);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("WORK", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Work);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("MSG", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.VoiceMessage);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("PREF", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Preferred);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("VOICE", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Voice);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("FAX", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Fax);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("CELL", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Cell);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("VIDEO", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Video);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("PAGER", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Pager);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("BBS", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.BBS);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("MODEM", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Modem);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("CAR", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Car);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("ISDN", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.ISDN);
				InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.Add("PCS", InboundVCardConverter.PhonePropertyHandler.PhoneTypes.PCS);
			}

			// Token: 0x06003A0C RID: 14860 RVA: 0x000EE490 File Offset: 0x000EC690
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				InboundVCardConverter.PhonePropertyHandler.PhoneTypes phoneTypes = InboundVCardConverter.PhonePropertyHandler.GetPhoneType(context.ApplicableTypes) | InboundVCardConverter.PhonePropertyHandler.GetPhoneType(context.UnnamedParameterValues);
				if (phoneTypes == InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					phoneTypes = InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Voice;
				}
				string value = reader.ReadValueAsString();
				if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Home) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Fax) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
					{
						context.Contact[ContactSchema.HomeFax] = value;
					}
					else
					{
						InboundVCardConverter.MultiPropertyTextHandler.SetArrayPropertyValue(value, context.Contact, new PropertyDefinition[]
						{
							ContactSchema.HomePhone,
							ContactSchema.HomePhone2
						});
					}
				}
				if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Work) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Fax) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
					{
						context.Contact[ContactSchema.WorkFax] = value;
					}
					else
					{
						InboundVCardConverter.MultiPropertyTextHandler.SetArrayPropertyValue(value, context.Contact, new PropertyDefinition[]
						{
							ContactSchema.BusinessPhoneNumber,
							ContactSchema.BusinessPhoneNumber2
						});
					}
				}
				if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Cell) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					context.Contact[ContactSchema.MobilePhone] = value;
				}
				if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Car) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					context.Contact[ContactSchema.CarPhone] = value;
				}
				if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.ISDN) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					context.Contact[ContactSchema.InternationalIsdnNumber] = value;
				}
				if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Preferred) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					context.Contact[ContactSchema.PrimaryTelephoneNumber] = value;
				}
				if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Pager) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					context.Contact[ContactSchema.Pager] = value;
				}
				if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Fax) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None && (phoneTypes & (InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Home | InboundVCardConverter.PhonePropertyHandler.PhoneTypes.Work)) == InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					context.Contact[ContactSchema.OtherFax] = value;
				}
				if ((phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.SpecificPropertyPromotion) == InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None || (phoneTypes & InboundVCardConverter.PhonePropertyHandler.PhoneTypes.OtherPropertyPromotion) != InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None)
				{
					context.Contact[ContactSchema.OtherTelephone] = value;
				}
			}

			// Token: 0x06003A0D RID: 14861 RVA: 0x000EE604 File Offset: 0x000EC804
			private static InboundVCardConverter.PhonePropertyHandler.PhoneTypes GetPhoneType(List<string> values)
			{
				InboundVCardConverter.PhonePropertyHandler.PhoneTypes phoneTypes = InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None;
				foreach (string key in values)
				{
					InboundVCardConverter.PhonePropertyHandler.PhoneTypes phoneTypes2 = InboundVCardConverter.PhonePropertyHandler.PhoneTypes.None;
					if (InboundVCardConverter.PhonePropertyHandler.phoneTypeMap.TryGetValue(key, out phoneTypes2))
					{
						phoneTypes |= phoneTypes2;
					}
				}
				return phoneTypes;
			}

			// Token: 0x04001F10 RID: 7952
			private static Dictionary<string, InboundVCardConverter.PhonePropertyHandler.PhoneTypes> phoneTypeMap = new Dictionary<string, InboundVCardConverter.PhonePropertyHandler.PhoneTypes>(StringComparer.OrdinalIgnoreCase);

			// Token: 0x0200056E RID: 1390
			[Flags]
			private enum PhoneTypes
			{
				// Token: 0x04001F12 RID: 7954
				None = 0,
				// Token: 0x04001F13 RID: 7955
				Home = 1,
				// Token: 0x04001F14 RID: 7956
				Work = 2,
				// Token: 0x04001F15 RID: 7957
				VoiceMessage = 4,
				// Token: 0x04001F16 RID: 7958
				Preferred = 8,
				// Token: 0x04001F17 RID: 7959
				Voice = 16,
				// Token: 0x04001F18 RID: 7960
				Fax = 32,
				// Token: 0x04001F19 RID: 7961
				Cell = 64,
				// Token: 0x04001F1A RID: 7962
				Video = 128,
				// Token: 0x04001F1B RID: 7963
				Pager = 256,
				// Token: 0x04001F1C RID: 7964
				BBS = 512,
				// Token: 0x04001F1D RID: 7965
				Modem = 1024,
				// Token: 0x04001F1E RID: 7966
				Car = 2048,
				// Token: 0x04001F1F RID: 7967
				ISDN = 4096,
				// Token: 0x04001F20 RID: 7968
				PCS = 8192,
				// Token: 0x04001F21 RID: 7969
				Default = 16,
				// Token: 0x04001F22 RID: 7970
				SpecificPropertyPromotion = 6507,
				// Token: 0x04001F23 RID: 7971
				OtherPropertyPromotion = 1668
			}
		}

		// Token: 0x0200056F RID: 1391
		private class EmailPropertyHandler : InboundVCardConverter.MultiPropertyTextHandler
		{
			// Token: 0x06003A0F RID: 14863 RVA: 0x000EE66C File Offset: 0x000EC86C
			static EmailPropertyHandler()
			{
				InboundVCardConverter.EmailPropertyHandler.typeToRoutingTypeMap.Add("INTERNET", InboundVCardConverter.EmailPropertyHandler.EmailType.SMTP);
				InboundVCardConverter.EmailPropertyHandler.typeToRoutingTypeMap.Add("X.400", InboundVCardConverter.EmailPropertyHandler.EmailType.X400);
				InboundVCardConverter.EmailPropertyHandler.typeToRoutingTypeMap.Add("IM", InboundVCardConverter.EmailPropertyHandler.EmailType.IM);
				InboundVCardConverter.EmailPropertyHandler.typeToRoutingTypeMap.Add("TLX", InboundVCardConverter.EmailPropertyHandler.EmailType.Telex);
				InboundVCardConverter.EmailPropertyHandler.emailSlots = new EmailAddressIndex[]
				{
					EmailAddressIndex.Email1,
					EmailAddressIndex.Email2,
					EmailAddressIndex.Email3
				};
			}

			// Token: 0x06003A10 RID: 14864 RVA: 0x000EE6E4 File Offset: 0x000EC8E4
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				bool flag = false;
				InboundVCardConverter.EmailPropertyHandler.EmailType emailType = InboundVCardConverter.EmailPropertyHandler.GetRoutingType(context.ApplicableTypes, ref flag);
				if (emailType == InboundVCardConverter.EmailPropertyHandler.EmailType.None)
				{
					emailType = InboundVCardConverter.EmailPropertyHandler.GetRoutingType(context.UnnamedParameterValues, ref flag);
				}
				if (emailType == InboundVCardConverter.EmailPropertyHandler.EmailType.None && !flag)
				{
					emailType = InboundVCardConverter.EmailPropertyHandler.EmailType.SMTP;
				}
				if (emailType == InboundVCardConverter.EmailPropertyHandler.EmailType.None || emailType == InboundVCardConverter.EmailPropertyHandler.EmailType.X400)
				{
					return;
				}
				string text = reader.ReadValueAsString();
				switch (emailType)
				{
				case InboundVCardConverter.EmailPropertyHandler.EmailType.IM:
					InboundVCardConverter.MultiPropertyTextHandler.SetArrayPropertyValue(text, context.Contact, new PropertyDefinition[]
					{
						ContactSchema.IMAddress,
						ContactSchema.IMAddress2,
						ContactSchema.IMAddress3
					});
					return;
				case InboundVCardConverter.EmailPropertyHandler.EmailType.Telex:
					context.Contact[ContactSchema.TelexNumber] = text;
					return;
				}
				for (int i = 0; i < InboundVCardConverter.EmailPropertyHandler.emailSlots.Length; i++)
				{
					if (context.Contact.EmailAddresses[InboundVCardConverter.EmailPropertyHandler.emailSlots[i]] == null)
					{
						context.Contact.EmailAddresses[InboundVCardConverter.EmailPropertyHandler.emailSlots[i]] = InboundMimeHeadersParser.CreateParticipantFromMime(null, text, context.Options, true);
						return;
					}
				}
			}

			// Token: 0x06003A11 RID: 14865 RVA: 0x000EE7E4 File Offset: 0x000EC9E4
			private static InboundVCardConverter.EmailPropertyHandler.EmailType GetRoutingType(List<string> types, ref bool foundUnknownType)
			{
				foreach (string text in types)
				{
					InboundVCardConverter.EmailPropertyHandler.EmailType result;
					if (InboundVCardConverter.EmailPropertyHandler.typeToRoutingTypeMap.TryGetValue(text, out result))
					{
						return result;
					}
					if (!string.Equals(text, "PREF", StringComparison.OrdinalIgnoreCase))
					{
						foundUnknownType = true;
					}
				}
				return InboundVCardConverter.EmailPropertyHandler.EmailType.None;
			}

			// Token: 0x04001F24 RID: 7972
			private static Dictionary<string, InboundVCardConverter.EmailPropertyHandler.EmailType> typeToRoutingTypeMap = new Dictionary<string, InboundVCardConverter.EmailPropertyHandler.EmailType>(StringComparer.OrdinalIgnoreCase);

			// Token: 0x04001F25 RID: 7973
			private static EmailAddressIndex[] emailSlots;

			// Token: 0x02000570 RID: 1392
			private enum EmailType
			{
				// Token: 0x04001F27 RID: 7975
				None,
				// Token: 0x04001F28 RID: 7976
				SMTP,
				// Token: 0x04001F29 RID: 7977
				X400,
				// Token: 0x04001F2A RID: 7978
				IM,
				// Token: 0x04001F2B RID: 7979
				Telex
			}
		}

		// Token: 0x02000571 RID: 1393
		private class AgentPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x06003A13 RID: 14867 RVA: 0x000EE85C File Offset: 0x000ECA5C
			public AgentPropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder | InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
			{
			}

			// Token: 0x06003A14 RID: 14868 RVA: 0x000EE868 File Offset: 0x000ECA68
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				if (reader.ValueType != ContactValueType.VCard)
				{
					return;
				}
				using (Stream valueReadStream = reader.GetValueReadStream())
				{
					InboundVCardConverter.Convert(valueReadStream, context.OverrideEncoding ?? context.Encoding, context.Contact, context.Options, InboundVCardConverter.agentHandlersMap);
				}
			}
		}

		// Token: 0x02000572 RID: 1394
		private class OrgPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x06003A15 RID: 14869 RVA: 0x000EE8CC File Offset: 0x000ECACC
			public OrgPropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder | InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
			{
			}

			// Token: 0x06003A16 RID: 14870 RVA: 0x000EE8D8 File Offset: 0x000ECAD8
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				if (reader.ReadNextValue())
				{
					string value = reader.ReadValueAsString(ContactValueSeparators.Semicolon);
					context.Contact[ContactSchema.CompanyName] = value;
					if (reader.ReadNextValue())
					{
						string value2 = reader.ReadValueAsString(ContactValueSeparators.None);
						context.Contact[ContactSchema.Department] = value2;
					}
				}
			}
		}

		// Token: 0x02000573 RID: 1395
		private class NotePropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x06003A17 RID: 14871 RVA: 0x000EE92B File Offset: 0x000ECB2B
			public NotePropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder | InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
			{
			}

			// Token: 0x06003A18 RID: 14872 RVA: 0x000EE934 File Offset: 0x000ECB34
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				using (Stream valueReadStream = reader.GetValueReadStream())
				{
					Encoding encoding = context.OverrideEncoding ?? context.Encoding;
					BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(BodyFormat.TextPlain, encoding.WebName);
					bodyWriteConfiguration.SetTargetFormat(BodyFormat.ApplicationRtf, encoding.WebName);
					using (Stream stream = context.Contact.Body.OpenWriteStream(bodyWriteConfiguration))
					{
						Util.StreamHandler.CopyStreamData(valueReadStream, stream);
					}
				}
			}
		}

		// Token: 0x02000574 RID: 1396
		private class UrlPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x06003A19 RID: 14873 RVA: 0x000EE9C4 File Offset: 0x000ECBC4
			public UrlPropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder)
			{
			}

			// Token: 0x06003A1A RID: 14874 RVA: 0x000EE9D0 File Offset: 0x000ECBD0
			private static void MarkWorkHomeUrl(List<string> types, ref bool isHomeUrl, ref bool isWorkUrl)
			{
				foreach (string a in types)
				{
					if (string.Equals(a, "HOME", StringComparison.OrdinalIgnoreCase))
					{
						isHomeUrl = true;
					}
					else if (string.Equals(a, "WORK", StringComparison.OrdinalIgnoreCase))
					{
						isWorkUrl = true;
					}
				}
			}

			// Token: 0x06003A1B RID: 14875 RVA: 0x000EEA3C File Offset: 0x000ECC3C
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				bool flag = false;
				bool flag2 = false;
				InboundVCardConverter.UrlPropertyHandler.MarkWorkHomeUrl(context.ApplicableTypes, ref flag, ref flag2);
				InboundVCardConverter.UrlPropertyHandler.MarkWorkHomeUrl(context.UnnamedParameterValues, ref flag, ref flag2);
				if (!flag && !flag2)
				{
					if (!(context.Contact.TryGetProperty(InternalSchema.PersonalHomePage) is string))
					{
						flag = true;
					}
					else if (!(context.Contact.TryGetProperty(InternalSchema.BusinessHomePage) is string))
					{
						flag2 = true;
					}
				}
				string value = reader.ReadValueAsString();
				if (flag)
				{
					context.Contact[ContactSchema.PersonalHomePage] = value;
				}
				if (flag2)
				{
					context.Contact[ContactSchema.BusinessHomePage] = value;
				}
			}
		}

		// Token: 0x02000575 RID: 1397
		private class SensitivityPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x06003A1C RID: 14876 RVA: 0x000EEAD5 File Offset: 0x000ECCD5
			public SensitivityPropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.None)
			{
			}

			// Token: 0x06003A1D RID: 14877 RVA: 0x000EEAE0 File Offset: 0x000ECCE0
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				string a = reader.ReadValueAsString();
				if (string.Equals(a, "PUBLIC", StringComparison.OrdinalIgnoreCase))
				{
					context.Contact[ItemSchema.Sensitivity] = Sensitivity.Normal;
					return;
				}
				if (string.Equals(a, "PRIVATE", StringComparison.OrdinalIgnoreCase))
				{
					context.Contact[ItemSchema.Sensitivity] = Sensitivity.Private;
					return;
				}
				if (string.Equals(a, "CONFIDENTIAL", StringComparison.OrdinalIgnoreCase))
				{
					context.Contact[ItemSchema.Sensitivity] = Sensitivity.CompanyConfidential;
				}
			}
		}

		// Token: 0x02000576 RID: 1398
		private class KeyPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x06003A1E RID: 14878 RVA: 0x000EEB63 File Offset: 0x000ECD63
			public KeyPropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.MustHaveDecoder)
			{
			}

			// Token: 0x06003A1F RID: 14879 RVA: 0x000EEB6C File Offset: 0x000ECD6C
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				if (reader.ValueType != ContactValueType.Binary)
				{
					return;
				}
				if (InboundVCardConverter.KeyPropertyHandler.IsX509Type(context.ApplicableTypes) || InboundVCardConverter.KeyPropertyHandler.IsX509Type(context.UnnamedParameterValues) || (context.ApplicableTypes.Count == 0 && context.UnnamedParameterValues.Count == 0))
				{
					using (reader.GetValueReadStream())
					{
					}
				}
			}

			// Token: 0x06003A20 RID: 14880 RVA: 0x000EEBDC File Offset: 0x000ECDDC
			private static bool IsX509Type(List<string> values)
			{
				foreach (string a in values)
				{
					if (string.Equals(a, "X509", StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x02000577 RID: 1399
		private class OutlookDesignPropertyHandler : InboundVCardConverter.TextStreamPropertyHandler
		{
			// Token: 0x06003A21 RID: 14881 RVA: 0x000EEC38 File Offset: 0x000ECE38
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
			}
		}

		// Token: 0x02000578 RID: 1400
		private class MsTextPropertyHandler : InboundVCardConverter.TextStreamPropertyHandler
		{
			// Token: 0x06003A23 RID: 14883 RVA: 0x000EEC44 File Offset: 0x000ECE44
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				using (Stream unicodeReadStream = InboundVCardConverter.TextStreamPropertyHandler.GetUnicodeReadStream(reader, context))
				{
					for (int i = 0; i < InboundVCardConverter.MsTextPropertyHandler.props.Length; i++)
					{
						PropertyError propertyError = context.Contact.TryGetProperty(InboundVCardConverter.MsTextPropertyHandler.props[i]) as PropertyError;
						if (propertyError != null && propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
						{
							using (Stream stream = context.Contact.OpenPropertyStream(InboundVCardConverter.MsTextPropertyHandler.props[i], PropertyOpenMode.Create))
							{
								Util.StreamHandler.CopyStreamData(unicodeReadStream, stream);
								break;
							}
						}
					}
				}
			}

			// Token: 0x04001F2C RID: 7980
			private static PropertyDefinition[] props = new PropertyDefinition[]
			{
				ContactSchema.UserText1,
				ContactSchema.UserText2,
				ContactSchema.UserText3,
				ContactSchema.UserText4
			};
		}

		// Token: 0x02000579 RID: 1401
		private class ImAddressPropertyHandler : InboundVCardConverter.MultiPropertyTextHandler
		{
			// Token: 0x06003A26 RID: 14886 RVA: 0x000EED24 File Offset: 0x000ECF24
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				InboundVCardConverter.MultiPropertyTextHandler.SetArrayPropertyValue(reader.ReadValueAsString(), context.Contact, new PropertyDefinition[]
				{
					ContactSchema.IMAddress,
					ContactSchema.IMAddress2,
					ContactSchema.IMAddress3
				});
			}
		}

		// Token: 0x0200057A RID: 1402
		private class MsTelPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x06003A28 RID: 14888 RVA: 0x000EED6C File Offset: 0x000ECF6C
			static MsTelPropertyHandler()
			{
				InboundVCardConverter.MsTelPropertyHandler.telTypesMap.Add("ASSISTANT", InboundVCardConverter.MsTelPropertyHandler.TelTypes.Assistant);
				InboundVCardConverter.MsTelPropertyHandler.telTypesMap.Add("TTYTDD", InboundVCardConverter.MsTelPropertyHandler.TelTypes.TTY);
				InboundVCardConverter.MsTelPropertyHandler.telTypesMap.Add("COMPANY", InboundVCardConverter.MsTelPropertyHandler.TelTypes.Company);
				InboundVCardConverter.MsTelPropertyHandler.telTypesMap.Add("CALLBACK", InboundVCardConverter.MsTelPropertyHandler.TelTypes.Callback);
				InboundVCardConverter.MsTelPropertyHandler.telTypesMap.Add("RADIO", InboundVCardConverter.MsTelPropertyHandler.TelTypes.Radio);
				InboundVCardConverter.MsTelPropertyHandler.typeToPropMap = new List<Pair<InboundVCardConverter.MsTelPropertyHandler.TelTypes, PropertyDefinition>>();
				InboundVCardConverter.MsTelPropertyHandler.typeToPropMap.Add(new Pair<InboundVCardConverter.MsTelPropertyHandler.TelTypes, PropertyDefinition>(InboundVCardConverter.MsTelPropertyHandler.TelTypes.Assistant, ContactSchema.AssistantPhoneNumber));
				InboundVCardConverter.MsTelPropertyHandler.typeToPropMap.Add(new Pair<InboundVCardConverter.MsTelPropertyHandler.TelTypes, PropertyDefinition>(InboundVCardConverter.MsTelPropertyHandler.TelTypes.Callback, ContactSchema.CallbackPhone));
				InboundVCardConverter.MsTelPropertyHandler.typeToPropMap.Add(new Pair<InboundVCardConverter.MsTelPropertyHandler.TelTypes, PropertyDefinition>(InboundVCardConverter.MsTelPropertyHandler.TelTypes.Company, ContactSchema.OrganizationMainPhone));
				InboundVCardConverter.MsTelPropertyHandler.typeToPropMap.Add(new Pair<InboundVCardConverter.MsTelPropertyHandler.TelTypes, PropertyDefinition>(InboundVCardConverter.MsTelPropertyHandler.TelTypes.Radio, ContactSchema.RadioPhone));
				InboundVCardConverter.MsTelPropertyHandler.typeToPropMap.Add(new Pair<InboundVCardConverter.MsTelPropertyHandler.TelTypes, PropertyDefinition>(InboundVCardConverter.MsTelPropertyHandler.TelTypes.TTY, ContactSchema.TtyTddPhoneNumber));
			}

			// Token: 0x06003A29 RID: 14889 RVA: 0x000EEE4D File Offset: 0x000ED04D
			public MsTelPropertyHandler() : base(InboundVCardConverter.PropertyHandler.HandlerOptions.None)
			{
			}

			// Token: 0x06003A2A RID: 14890 RVA: 0x000EEE58 File Offset: 0x000ED058
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				InboundVCardConverter.MsTelPropertyHandler.TelTypes telTypes = InboundVCardConverter.MsTelPropertyHandler.GetTelTypes(context.ApplicableTypes) | InboundVCardConverter.MsTelPropertyHandler.GetTelTypes(context.UnnamedParameterValues);
				string value = reader.ReadValueAsString();
				foreach (Pair<InboundVCardConverter.MsTelPropertyHandler.TelTypes, PropertyDefinition> pair in InboundVCardConverter.MsTelPropertyHandler.typeToPropMap)
				{
					if ((telTypes & pair.First) != InboundVCardConverter.MsTelPropertyHandler.TelTypes.None)
					{
						context.Contact[pair.Second] = value;
					}
				}
			}

			// Token: 0x06003A2B RID: 14891 RVA: 0x000EEEE0 File Offset: 0x000ED0E0
			private static InboundVCardConverter.MsTelPropertyHandler.TelTypes GetTelTypes(List<string> values)
			{
				InboundVCardConverter.MsTelPropertyHandler.TelTypes telTypes = InboundVCardConverter.MsTelPropertyHandler.TelTypes.None;
				foreach (string key in values)
				{
					InboundVCardConverter.MsTelPropertyHandler.TelTypes telTypes2 = InboundVCardConverter.MsTelPropertyHandler.TelTypes.None;
					if (InboundVCardConverter.MsTelPropertyHandler.telTypesMap.TryGetValue(key, out telTypes2))
					{
						telTypes |= telTypes2;
					}
				}
				return telTypes;
			}

			// Token: 0x04001F2D RID: 7981
			private static Dictionary<string, InboundVCardConverter.MsTelPropertyHandler.TelTypes> telTypesMap = new Dictionary<string, InboundVCardConverter.MsTelPropertyHandler.TelTypes>(StringComparer.OrdinalIgnoreCase);

			// Token: 0x04001F2E RID: 7982
			private static List<Pair<InboundVCardConverter.MsTelPropertyHandler.TelTypes, PropertyDefinition>> typeToPropMap;

			// Token: 0x0200057B RID: 1403
			[Flags]
			private enum TelTypes
			{
				// Token: 0x04001F30 RID: 7984
				None = 0,
				// Token: 0x04001F31 RID: 7985
				Assistant = 1,
				// Token: 0x04001F32 RID: 7986
				TTY = 2,
				// Token: 0x04001F33 RID: 7987
				Company = 4,
				// Token: 0x04001F34 RID: 7988
				Callback = 8,
				// Token: 0x04001F35 RID: 7989
				Radio = 16
			}
		}

		// Token: 0x0200057C RID: 1404
		private class MultiStringPropertyHandler : InboundVCardConverter.PropertyHandler
		{
			// Token: 0x06003A2C RID: 14892 RVA: 0x000EEF40 File Offset: 0x000ED140
			public MultiStringPropertyHandler(StorePropertyDefinition prop) : base(InboundVCardConverter.PropertyHandler.HandlerOptions.CanHaveDecoder | InboundVCardConverter.PropertyHandler.HandlerOptions.CanOverrideCharset)
			{
				this.prop = prop;
			}

			// Token: 0x06003A2D RID: 14893 RVA: 0x000EEF50 File Offset: 0x000ED150
			protected override void InternalProcessPropertyValue(InboundVCardConverter.ProcessingContext context, ContactPropertyReader reader)
			{
				string[] valueOrDefault = context.Contact.GetValueOrDefault<string[]>(this.prop, InboundVCardConverter.MultiStringPropertyHandler.emptyValue);
				List<string> list = new List<string>(valueOrDefault);
				while (reader.ReadNextValue())
				{
					list.Add(reader.ReadValueAsString(ContactValueSeparators.Comma | ContactValueSeparators.Semicolon));
				}
				context.Contact[this.prop] = list.ToArray();
			}

			// Token: 0x04001F36 RID: 7990
			private readonly StorePropertyDefinition prop;

			// Token: 0x04001F37 RID: 7991
			private static string[] emptyValue = Array<string>.Empty;
		}
	}
}
