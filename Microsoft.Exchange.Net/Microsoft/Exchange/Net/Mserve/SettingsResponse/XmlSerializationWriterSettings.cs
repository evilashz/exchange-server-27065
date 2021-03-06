using System;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008FC RID: 2300
	internal class XmlSerializationWriterSettings : XmlSerializationWriter
	{
		// Token: 0x06003189 RID: 12681 RVA: 0x000736E0 File Offset: 0x000718E0
		public void Write43_Settings(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteEmptyTag("Settings", "HMSETTINGS:");
				return;
			}
			base.TopLevelElement();
			this.Write42_Settings("Settings", "HMSETTINGS:", (Settings)o, false, false);
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x0007371C File Offset: 0x0007191C
		private void Write42_Settings(string n, string ns, Settings o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(Settings)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementStringRaw("Status", "HMSETTINGS:", XmlConvert.ToString(o.Status));
			this.Write2_SettingsFault("Fault", "HMSETTINGS:", o.Fault, false, false);
			this.Write3_SettingsAuthPolicy("AuthPolicy", "HMSETTINGS:", o.AuthPolicy, false, false);
			this.Write10_SettingsServiceSettings("ServiceSettings", "HMSETTINGS:", o.ServiceSettings, false, false);
			this.Write41_SettingsAccountSettings("AccountSettings", "HMSETTINGS:", o.AccountSettings, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000737FC File Offset: 0x000719FC
		private void Write41_SettingsAccountSettings(string n, string ns, SettingsAccountSettings o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsAccountSettings)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			if (o.StatusSpecified)
			{
				base.WriteElementStringRaw("Status", "HMSETTINGS:", XmlConvert.ToString(o.Status));
			}
			if (o.Item != null)
			{
				if (o.Item is SettingsAccountSettingsSet)
				{
					this.Write40_SettingsAccountSettingsSet("Set", "HMSETTINGS:", (SettingsAccountSettingsSet)o.Item, false, false);
				}
				else
				{
					if (!(o.Item is SettingsAccountSettingsGet))
					{
						throw base.CreateUnknownTypeException(o.Item);
					}
					this.Write33_SettingsAccountSettingsGet("Get", "HMSETTINGS:", (SettingsAccountSettingsGet)o.Item, false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000738F0 File Offset: 0x00071AF0
		private void Write33_SettingsAccountSettingsGet(string n, string ns, SettingsAccountSettingsGet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsAccountSettingsGet)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			FiltersResponseTypeFilter[] filters = o.Filters;
			if (filters != null)
			{
				base.WriteStartElement("Filters", "HMSETTINGS:", null, false);
				for (int i = 0; i < filters.Length; i++)
				{
					this.Write19_FiltersResponseTypeFilter("Filter", "HMSETTINGS:", filters[i], false, false);
				}
				base.WriteEndElement();
			}
			ListsGetResponseTypeList[] lists = o.Lists;
			if (lists != null)
			{
				base.WriteStartElement("Lists", "HMSETTINGS:", null, false);
				for (int j = 0; j < lists.Length; j++)
				{
					this.Write7_ListsGetResponseTypeList("List", "HMSETTINGS:", lists[j], false, false);
				}
				base.WriteEndElement();
			}
			this.Write29_OptionsType("Options", "HMSETTINGS:", o.Options, false, false);
			this.Write32_PropertiesType("Properties", "HMSETTINGS:", o.Properties, false, false);
			this.Write14_StringWithVersionType("UserSignature", "HMSETTINGS:", o.UserSignature, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x00073A2C File Offset: 0x00071C2C
		private void Write14_StringWithVersionType(string n, string ns, StringWithVersionType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(StringWithVersionType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("StringWithVersionType", "HMSETTINGS:");
			}
			base.WriteAttribute("version", "", XmlConvert.ToString(o.version));
			if (o.Value != null)
			{
				base.WriteValue(o.Value);
			}
			base.WriteEndElement(o);
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x00073AC4 File Offset: 0x00071CC4
		private void Write32_PropertiesType(string n, string ns, PropertiesType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(PropertiesType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("PropertiesType", "HMSETTINGS:");
			}
			base.WriteElementString("AccountStatus", "HMSETTINGS:", this.Write30_AccountStatusType(o.AccountStatus));
			base.WriteElementString("ParentalControlStatus", "HMSETTINGS:", this.Write31_ParentalControlStatusType(o.ParentalControlStatus));
			base.WriteElementStringRaw("MailBoxSize", "HMSETTINGS:", XmlConvert.ToString(o.MailBoxSize));
			base.WriteElementStringRaw("MaxMailBoxSize", "HMSETTINGS:", XmlConvert.ToString(o.MaxMailBoxSize));
			base.WriteElementStringRaw("MaxAttachments", "HMSETTINGS:", XmlConvert.ToString(o.MaxAttachments));
			base.WriteElementStringRaw("MaxMessageSize", "HMSETTINGS:", XmlConvert.ToString(o.MaxMessageSize));
			base.WriteElementStringRaw("MaxUnencodedMessageSize", "HMSETTINGS:", XmlConvert.ToString(o.MaxUnencodedMessageSize));
			base.WriteElementStringRaw("MaxFilters", "HMSETTINGS:", XmlConvert.ToString(o.MaxFilters));
			base.WriteElementStringRaw("MaxFilterClauseValueLength", "HMSETTINGS:", XmlConvert.ToString(o.MaxFilterClauseValueLength));
			base.WriteElementStringRaw("MaxRecipients", "HMSETTINGS:", XmlConvert.ToString(o.MaxRecipients));
			base.WriteElementStringRaw("MaxUserSignatureLength", "HMSETTINGS:", XmlConvert.ToString(o.MaxUserSignatureLength));
			base.WriteElementStringRaw("BlockListAddressMax", "HMSETTINGS:", XmlConvert.ToString(o.BlockListAddressMax));
			base.WriteElementStringRaw("BlockListDomainMax", "HMSETTINGS:", XmlConvert.ToString(o.BlockListDomainMax));
			base.WriteElementStringRaw("WhiteListAddressMax", "HMSETTINGS:", XmlConvert.ToString(o.WhiteListAddressMax));
			base.WriteElementStringRaw("WhiteListDomainMax", "HMSETTINGS:", XmlConvert.ToString(o.WhiteListDomainMax));
			base.WriteElementStringRaw("WhiteToListMax", "HMSETTINGS:", XmlConvert.ToString(o.WhiteToListMax));
			base.WriteElementStringRaw("AlternateFromListMax", "HMSETTINGS:", XmlConvert.ToString(o.AlternateFromListMax));
			base.WriteElementStringRaw("MaxDailySendMessages", "HMSETTINGS:", XmlConvert.ToString(o.MaxDailySendMessages));
			base.WriteEndElement(o);
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x00073D14 File Offset: 0x00071F14
		private string Write31_ParentalControlStatusType(ParentalControlStatusType v)
		{
			string result;
			switch (v)
			{
			case ParentalControlStatusType.None:
				result = "None";
				break;
			case ParentalControlStatusType.FullAccess:
				result = "FullAccess";
				break;
			case ParentalControlStatusType.RestrictedAccess:
				result = "RestrictedAccess";
				break;
			case ParentalControlStatusType.NoAccess:
				result = "NoAccess";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.ParentalControlStatusType");
			}
			return result;
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x00073D7C File Offset: 0x00071F7C
		private string Write30_AccountStatusType(AccountStatusType v)
		{
			string result;
			switch (v)
			{
			case AccountStatusType.OK:
				result = "OK";
				break;
			case AccountStatusType.Blocked:
				result = "Blocked";
				break;
			case AccountStatusType.RequiresHIP:
				result = "RequiresHIP";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.AccountStatusType");
			}
			return result;
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x00073DD8 File Offset: 0x00071FD8
		private void Write29_OptionsType(string n, string ns, OptionsType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(OptionsType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("OptionsType", "HMSETTINGS:");
			}
			base.WriteElementStringRaw("ConfirmSent", "HMSETTINGS:", XmlConvert.ToString(o.ConfirmSent));
			base.WriteElementString("HeaderDisplay", "HMSETTINGS:", this.Write20_HeaderDisplayType(o.HeaderDisplay));
			base.WriteElementString("IncludeOriginalInReply", "HMSETTINGS:", this.Write21_IncludeOriginalInReplyType(o.IncludeOriginalInReply));
			base.WriteElementString("JunkLevel", "HMSETTINGS:", this.Write22_JunkLevelType(o.JunkLevel));
			base.WriteElementString("JunkMailDestination", "HMSETTINGS:", this.Write23_JunkMailDestinationType(o.JunkMailDestination));
			base.WriteElementString("ReplyIndicator", "HMSETTINGS:", this.Write24_ReplyIndicatorType(o.ReplyIndicator));
			base.WriteElementString("ReplyToAddress", "HMSETTINGS:", o.ReplyToAddress);
			base.WriteElementStringRaw("SaveSentMail", "HMSETTINGS:", XmlConvert.ToString(o.SaveSentMail));
			base.WriteElementStringRaw("UseReplyToAddress", "HMSETTINGS:", XmlConvert.ToString(o.UseReplyToAddress));
			this.Write26_OptionsTypeVacationResponse("VacationResponse", "HMSETTINGS:", o.VacationResponse, false, false);
			this.Write28_OptionsTypeMailForwarding("MailForwarding", "HMSETTINGS:", o.MailForwarding, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x00073F64 File Offset: 0x00072164
		private void Write28_OptionsTypeMailForwarding(string n, string ns, OptionsTypeMailForwarding o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(OptionsTypeMailForwarding)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementString("Mode", "HMSETTINGS:", this.Write27_ForwardingMode(o.Mode));
			string[] addresses = o.Addresses;
			if (addresses != null)
			{
				base.WriteStartElement("Addresses", "HMSETTINGS:", null, false);
				for (int i = 0; i < addresses.Length; i++)
				{
					base.WriteElementString("Address", "HMSETTINGS:", addresses[i]);
				}
				base.WriteEndElement();
			}
			base.WriteEndElement(o);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x00074028 File Offset: 0x00072228
		private string Write27_ForwardingMode(ForwardingMode v)
		{
			string result;
			switch (v)
			{
			case ForwardingMode.NoForwarding:
				result = "NoForwarding";
				break;
			case ForwardingMode.ForwardOnly:
				result = "ForwardOnly";
				break;
			case ForwardingMode.StoreAndForward:
				result = "StoreAndForward";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.ForwardingMode");
			}
			return result;
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x00074084 File Offset: 0x00072284
		private void Write26_OptionsTypeVacationResponse(string n, string ns, OptionsTypeVacationResponse o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(OptionsTypeVacationResponse)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementString("Mode", "HMSETTINGS:", this.Write25_VacationResponseMode(o.Mode));
			base.WriteElementString("StartTime", "HMSETTINGS:", o.StartTime);
			base.WriteElementString("EndTime", "HMSETTINGS:", o.EndTime);
			base.WriteElementString("Message", "HMSETTINGS:", o.Message);
			base.WriteEndElement(o);
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x00074148 File Offset: 0x00072348
		private string Write25_VacationResponseMode(VacationResponseMode v)
		{
			string result;
			switch (v)
			{
			case VacationResponseMode.NoVacationResponse:
				result = "NoVacationResponse";
				break;
			case VacationResponseMode.OncePerSender:
				result = "OncePerSender";
				break;
			case VacationResponseMode.OncePerContact:
				result = "OncePerContact";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.VacationResponseMode");
			}
			return result;
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000741A4 File Offset: 0x000723A4
		private string Write24_ReplyIndicatorType(ReplyIndicatorType v)
		{
			string result;
			switch (v)
			{
			case ReplyIndicatorType.None:
				result = "None";
				break;
			case ReplyIndicatorType.Line:
				result = "Line";
				break;
			case ReplyIndicatorType.Arrow:
				result = "Arrow";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.ReplyIndicatorType");
			}
			return result;
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x00074200 File Offset: 0x00072400
		private string Write23_JunkMailDestinationType(JunkMailDestinationType v)
		{
			string result;
			switch (v)
			{
			case JunkMailDestinationType.ImmediateDeletion:
				result = "Immediate Deletion";
				break;
			case JunkMailDestinationType.JunkMail:
				result = "Junk Mail";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.JunkMailDestinationType");
			}
			return result;
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x00074250 File Offset: 0x00072450
		private string Write22_JunkLevelType(JunkLevelType v)
		{
			string result;
			switch (v)
			{
			case JunkLevelType.Off:
				result = "Off";
				break;
			case JunkLevelType.Low:
				result = "Low";
				break;
			case JunkLevelType.High:
				result = "High";
				break;
			case JunkLevelType.Exclusive:
				result = "Exclusive";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.JunkLevelType");
			}
			return result;
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000742B8 File Offset: 0x000724B8
		private string Write21_IncludeOriginalInReplyType(IncludeOriginalInReplyType v)
		{
			string result;
			switch (v)
			{
			case IncludeOriginalInReplyType.Auto:
				result = "Auto";
				break;
			case IncludeOriginalInReplyType.Manual:
				result = "Manual";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.IncludeOriginalInReplyType");
			}
			return result;
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x00074308 File Offset: 0x00072508
		private string Write20_HeaderDisplayType(HeaderDisplayType v)
		{
			string result;
			switch (v)
			{
			case HeaderDisplayType.NoHeader:
				result = "No Header";
				break;
			case HeaderDisplayType.Basic:
				result = "Basic";
				break;
			case HeaderDisplayType.Full:
				result = "Full";
				break;
			case HeaderDisplayType.Advanced:
				result = "Advanced";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.HeaderDisplayType");
			}
			return result;
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x00074370 File Offset: 0x00072570
		private void Write7_ListsGetResponseTypeList(string n, string ns, ListsGetResponseTypeList o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ListsGetResponseTypeList)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteAttribute("name", "", o.name);
			string[] addresses = o.Addresses;
			if (addresses != null)
			{
				base.WriteStartElement("Addresses", "HMSETTINGS:", null, false);
				for (int i = 0; i < addresses.Length; i++)
				{
					base.WriteElementString("Address", "HMSETTINGS:", addresses[i]);
				}
				base.WriteEndElement();
			}
			string[] domains = o.Domains;
			if (domains != null)
			{
				base.WriteStartElement("Domains", "HMSETTINGS:", null, false);
				for (int j = 0; j < domains.Length; j++)
				{
					base.WriteElementString("Domain", "HMSETTINGS:", domains[j]);
				}
				base.WriteEndElement();
			}
			string[] localParts = o.LocalParts;
			if (localParts != null)
			{
				base.WriteStartElement("LocalParts", "HMSETTINGS:", null, false);
				for (int k = 0; k < localParts.Length; k++)
				{
					base.WriteElementString("LocalPart", "HMSETTINGS:", localParts[k]);
				}
				base.WriteEndElement();
			}
			base.WriteEndElement(o);
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000744C4 File Offset: 0x000726C4
		private void Write19_FiltersResponseTypeFilter(string n, string ns, FiltersResponseTypeFilter o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(FiltersResponseTypeFilter)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementStringRaw("ExecutionOrder", "HMSETTINGS:", XmlConvert.ToString(o.ExecutionOrder));
			base.WriteElementStringRaw("Enabled", "HMSETTINGS:", XmlConvert.ToString(o.Enabled));
			base.WriteElementString("RunWhen", "HMSETTINGS:", this.Write11_RunWhenType(o.RunWhen));
			this.Write16_Item("Condition", "HMSETTINGS:", o.Condition, false, false);
			this.Write18_Item("Actions", "HMSETTINGS:", o.Actions, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x000745AC File Offset: 0x000727AC
		private void Write18_Item(string n, string ns, FiltersResponseTypeFilterActions o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(FiltersResponseTypeFilterActions)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			this.Write17_Item("MoveToFolder", "HMSETTINGS:", o.MoveToFolder, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x00074628 File Offset: 0x00072828
		private void Write17_Item(string n, string ns, FiltersResponseTypeFilterActionsMoveToFolder o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(FiltersResponseTypeFilterActionsMoveToFolder)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementString("FolderId", "HMSETTINGS:", o.FolderId);
			base.WriteEndElement(o);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000746A4 File Offset: 0x000728A4
		private void Write16_Item(string n, string ns, FiltersResponseTypeFilterCondition o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(FiltersResponseTypeFilterCondition)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			this.Write15_Item("Clause", "HMSETTINGS:", o.Clause, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x00074720 File Offset: 0x00072920
		private void Write15_Item(string n, string ns, FiltersResponseTypeFilterConditionClause o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(FiltersResponseTypeFilterConditionClause)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementString("Field", "HMSETTINGS:", this.Write12_FilterKeyType(o.Field));
			base.WriteElementString("Operator", "HMSETTINGS:", this.Write13_FilterOperatorType(o.Operator));
			this.Write14_StringWithVersionType("Value", "HMSETTINGS:", o.Value, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000747D4 File Offset: 0x000729D4
		private string Write13_FilterOperatorType(FilterOperatorType v)
		{
			string result;
			switch (v)
			{
			case FilterOperatorType.Contains:
				result = "Contains";
				break;
			case FilterOperatorType.Doesnotcontain:
				result = "Does not contain";
				break;
			case FilterOperatorType.Containsword:
				result = "Contains word";
				break;
			case FilterOperatorType.Startswith:
				result = "Starts with";
				break;
			case FilterOperatorType.Endswith:
				result = "Ends with";
				break;
			case FilterOperatorType.Equals:
				result = "Equals";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.FilterOperatorType");
			}
			return result;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x00074854 File Offset: 0x00072A54
		private string Write12_FilterKeyType(FilterKeyType v)
		{
			string result;
			switch (v)
			{
			case FilterKeyType.Subject:
				result = "Subject";
				break;
			case FilterKeyType.FromName:
				result = "From Name";
				break;
			case FilterKeyType.FromAddress:
				result = "From Address";
				break;
			case FilterKeyType.ToorCCLine:
				result = "To or CC Line";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.FilterKeyType");
			}
			return result;
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000748BC File Offset: 0x00072ABC
		private string Write11_RunWhenType(RunWhenType v)
		{
			string result;
			switch (v)
			{
			case RunWhenType.MessageReceived:
				result = "MessageReceived";
				break;
			case RunWhenType.MessageSent:
				result = "MessageSent";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "Microsoft.Exchange.Net.Mserve.SettingsResponse.RunWhenType");
			}
			return result;
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x0007490C File Offset: 0x00072B0C
		private void Write40_SettingsAccountSettingsSet(string n, string ns, SettingsAccountSettingsSet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsAccountSettingsSet)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			this.Write35_SettingsCategoryResponseType("Filters", "HMSETTINGS:", o.Filters, false, false);
			this.Write39_ListsSetResponseType("Lists", "HMSETTINGS:", o.Lists, false, false);
			this.Write35_SettingsCategoryResponseType("Options", "HMSETTINGS:", o.Options, false, false);
			this.Write35_SettingsCategoryResponseType("UserSignature", "HMSETTINGS:", o.UserSignature, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000749D0 File Offset: 0x00072BD0
		private void Write35_SettingsCategoryResponseType(string n, string ns, SettingsCategoryResponseType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsCategoryResponseType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SettingsCategoryResponseType", "HMSETTINGS:");
			}
			base.WriteElementStringRaw("Status", "HMSETTINGS:", XmlConvert.ToString(o.Status));
			this.Write34_Fault("Fault", "HMSETTINGS:", o.Fault, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x00074A6C File Offset: 0x00072C6C
		private void Write34_Fault(string n, string ns, Fault o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(Fault)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementString("Faultcode", "HMSETTINGS:", o.Faultcode);
			base.WriteElementString("Faultstring", "HMSETTINGS:", o.Faultstring);
			base.WriteElementString("Detail", "HMSETTINGS:", o.Detail);
			base.WriteEndElement(o);
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x00074B14 File Offset: 0x00072D14
		private void Write39_ListsSetResponseType(string n, string ns, ListsSetResponseType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ListsSetResponseType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ListsSetResponseType", "HMSETTINGS:");
			}
			base.WriteElementStringRaw("Status", "HMSETTINGS:", XmlConvert.ToString(o.Status));
			ListsSetResponseTypeList[] list = o.List;
			if (list != null)
			{
				for (int i = 0; i < list.Length; i++)
				{
					this.Write38_ListsSetResponseTypeList("List", "HMSETTINGS:", list[i], false, false);
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x00074BC4 File Offset: 0x00072DC4
		private void Write38_ListsSetResponseTypeList(string n, string ns, ListsSetResponseTypeList o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ListsSetResponseTypeList)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteAttribute("name", "", o.name);
			StatusType[] items = o.Items;
			if (items != null)
			{
				ItemsChoiceType[] itemsElementName = o.ItemsElementName;
				if (itemsElementName == null || itemsElementName.Length < items.Length)
				{
					throw base.CreateInvalidChoiceIdentifierValueException("Microsoft.Exchange.Net.Mserve.SettingsResponse.ItemsChoiceType", "ItemsElementName");
				}
				for (int i = 0; i < items.Length; i++)
				{
					StatusType statusType = items[i];
					ItemsChoiceType itemsChoiceType = itemsElementName[i];
					if (statusType != null)
					{
						if (itemsChoiceType == ItemsChoiceType.Add)
						{
							if (statusType != null && statusType == null)
							{
								throw base.CreateMismatchChoiceException("Microsoft.Exchange.Net.Mserve.SettingsResponse.StatusType", "ItemsElementName", "Microsoft.Exchange.Net.Mserve.SettingsResponse.ItemsChoiceType.@Add");
							}
							this.Write37_StatusType("Add", "HMSETTINGS:", statusType, false, false);
						}
						else if (itemsChoiceType == ItemsChoiceType.Set)
						{
							if (statusType != null && statusType == null)
							{
								throw base.CreateMismatchChoiceException("Microsoft.Exchange.Net.Mserve.SettingsResponse.StatusType", "ItemsElementName", "Microsoft.Exchange.Net.Mserve.SettingsResponse.ItemsChoiceType.@Set");
							}
							this.Write37_StatusType("Set", "HMSETTINGS:", statusType, false, false);
						}
						else
						{
							if (itemsChoiceType != ItemsChoiceType.Delete)
							{
								throw base.CreateUnknownTypeException(statusType);
							}
							if (statusType != null && statusType == null)
							{
								throw base.CreateMismatchChoiceException("Microsoft.Exchange.Net.Mserve.SettingsResponse.StatusType", "ItemsElementName", "Microsoft.Exchange.Net.Mserve.SettingsResponse.ItemsChoiceType.@Delete");
							}
							this.Write37_StatusType("Delete", "HMSETTINGS:", statusType, false, false);
						}
					}
				}
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x00074D48 File Offset: 0x00072F48
		private void Write37_StatusType(string n, string ns, StatusType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(StatusType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("StatusType", "HMSETTINGS:");
			}
			base.WriteElementStringRaw("Status", "HMSETTINGS:", XmlConvert.ToString(o.Status));
			this.Write34_Fault("Fault", "HMSETTINGS:", o.Fault, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x00074DE4 File Offset: 0x00072FE4
		private void Write10_SettingsServiceSettings(string n, string ns, SettingsServiceSettings o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsServiceSettings)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementStringRaw("Status", "HMSETTINGS:", XmlConvert.ToString(o.Status));
			this.Write4_RulesResponseType("SafetyLevelRules", "HMSETTINGS:", o.SafetyLevelRules, false, false);
			this.Write4_RulesResponseType("SafetyActions", "HMSETTINGS:", o.SafetyActions, false, false);
			this.Write6_Item("Properties", "HMSETTINGS:", o.Properties, false, false);
			this.Write9_SettingsServiceSettingsLists("Lists", "HMSETTINGS:", o.Lists, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x00074EC4 File Offset: 0x000730C4
		private void Write9_SettingsServiceSettingsLists(string n, string ns, SettingsServiceSettingsLists o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsServiceSettingsLists)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementStringRaw("Status", "HMSETTINGS:", XmlConvert.ToString(o.Status));
			this.Write8_Item("Get", "HMSETTINGS:", o.Get, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x00074F5C File Offset: 0x0007315C
		private void Write8_Item(string n, string ns, SettingsServiceSettingsListsGet o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsServiceSettingsListsGet)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			ListsGetResponseTypeList[] lists = o.Lists;
			if (lists != null)
			{
				base.WriteStartElement("Lists", "HMSETTINGS:", null, false);
				for (int i = 0; i < lists.Length; i++)
				{
					this.Write7_ListsGetResponseTypeList("List", "HMSETTINGS:", lists[i], false, false);
				}
				base.WriteEndElement();
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x00075008 File Offset: 0x00073208
		private void Write6_Item(string n, string ns, SettingsServiceSettingsProperties o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsServiceSettingsProperties)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementStringRaw("Status", "HMSETTINGS:", XmlConvert.ToString(o.Status));
			this.Write5_ServiceSettingsPropertiesType("Get", "HMSETTINGS:", o.Get, false, false);
			base.WriteEndElement(o);
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000750A0 File Offset: 0x000732A0
		private void Write5_ServiceSettingsPropertiesType(string n, string ns, ServiceSettingsPropertiesType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ServiceSettingsPropertiesType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ServiceSettingsPropertiesType", "HMSETTINGS:");
			}
			base.WriteElementStringRaw("ServiceTimeOut", "HMSETTINGS:", XmlConvert.ToString(o.ServiceTimeOut));
			base.WriteElementStringRaw("MinSyncPollInterval", "HMSETTINGS:", XmlConvert.ToString(o.MinSyncPollInterval));
			base.WriteElementStringRaw("MinSettingsPollInterval", "HMSETTINGS:", XmlConvert.ToString(o.MinSettingsPollInterval));
			base.WriteElementStringRaw("SyncMultiplier", "HMSETTINGS:", XmlConvert.ToString(o.SyncMultiplier));
			base.WriteElementStringRaw("MaxObjectsInSync", "HMSETTINGS:", XmlConvert.ToString(o.MaxObjectsInSync));
			base.WriteElementStringRaw("MaxNumberOfEmailAdds", "HMSETTINGS:", XmlConvert.ToString(o.MaxNumberOfEmailAdds));
			base.WriteElementStringRaw("MaxNumberOfFolderAdds", "HMSETTINGS:", XmlConvert.ToString(o.MaxNumberOfFolderAdds));
			base.WriteElementStringRaw("MaxNumberOfStatelessObjects", "HMSETTINGS:", XmlConvert.ToString(o.MaxNumberOfStatelessObjects));
			base.WriteElementStringRaw("DefaultStatelessEmailWindowSize", "HMSETTINGS:", XmlConvert.ToString(o.DefaultStatelessEmailWindowSize));
			base.WriteElementStringRaw("MaxStatelessEmailWindowSize", "HMSETTINGS:", XmlConvert.ToString(o.MaxStatelessEmailWindowSize));
			base.WriteElementStringRaw("MaxTotalLengthOfForwardingAddresses", "HMSETTINGS:", XmlConvert.ToString(o.MaxTotalLengthOfForwardingAddresses));
			base.WriteElementStringRaw("MaxVacationResponseMessageLength", "HMSETTINGS:", XmlConvert.ToString(o.MaxVacationResponseMessageLength));
			base.WriteElementString("MinVacationResponseStartTime", "HMSETTINGS:", o.MinVacationResponseStartTime);
			base.WriteElementString("MaxVacationResponseEndTime", "HMSETTINGS:", o.MaxVacationResponseEndTime);
			base.WriteElementStringRaw("MaxNumberOfProvisionCommands", "HMSETTINGS:", XmlConvert.ToString(o.MaxNumberOfProvisionCommands));
			base.WriteEndElement(o);
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x00075298 File Offset: 0x00073498
		private void Write4_RulesResponseType(string n, string ns, RulesResponseType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(RulesResponseType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("RulesResponseType", "HMSETTINGS:");
			}
			base.WriteElementStringRaw("Status", "HMSETTINGS:", XmlConvert.ToString(o.Status));
			if (o.Get != null || o.Get == null)
			{
				base.WriteElementLiteral(o.Get, "Get", "HMSETTINGS:", false, false);
				base.WriteElementString("Version", "HMSETTINGS:", o.Version);
				base.WriteEndElement(o);
				return;
			}
			throw base.CreateInvalidAnyTypeException(o.Get);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x00075368 File Offset: 0x00073568
		private void Write3_SettingsAuthPolicy(string n, string ns, SettingsAuthPolicy o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsAuthPolicy)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementString("SAP", "HMSETTINGS:", o.SAP);
			base.WriteElementString("Version", "HMSETTINGS:", o.Version);
			base.WriteEndElement(o);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000753F8 File Offset: 0x000735F8
		private void Write2_SettingsFault(string n, string ns, SettingsFault o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SettingsFault)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType(null, "HMSETTINGS:");
			}
			base.WriteElementString("Faultcode", "HMSETTINGS:", o.Faultcode);
			base.WriteElementString("Faultstring", "HMSETTINGS:", o.Faultstring);
			base.WriteElementString("Detail", "HMSETTINGS:", o.Detail);
			base.WriteEndElement(o);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x0007549E File Offset: 0x0007369E
		protected override void InitCallbacks()
		{
		}
	}
}
