using System;
using System.Xml;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A17 RID: 2583
	[Serializable]
	public class IMAPConnectionSettings : ConnectionSettingsBase
	{
		// Token: 0x06005F08 RID: 24328 RVA: 0x0019143E File Offset: 0x0018F63E
		public IMAPConnectionSettings()
		{
			this[SimpleProviderObjectSchema.Identity] = MigrationBatchId.Any;
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06005F09 RID: 24329 RVA: 0x00191456 File Offset: 0x0018F656
		public override MigrationType Type
		{
			get
			{
				return MigrationType.IMAP;
			}
		}

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x06005F0A RID: 24330 RVA: 0x00191459 File Offset: 0x0018F659
		// (set) Token: 0x06005F0B RID: 24331 RVA: 0x0019146B File Offset: 0x0018F66B
		public string Server
		{
			get
			{
				return (string)this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.Server];
			}
			set
			{
				this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.Server] = value;
			}
		}

		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x06005F0C RID: 24332 RVA: 0x00191479 File Offset: 0x0018F679
		// (set) Token: 0x06005F0D RID: 24333 RVA: 0x0019148B File Offset: 0x0018F68B
		public int Port
		{
			get
			{
				return (int)this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.Port];
			}
			set
			{
				this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.Port] = value;
			}
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06005F0E RID: 24334 RVA: 0x0019149E File Offset: 0x0018F69E
		// (set) Token: 0x06005F0F RID: 24335 RVA: 0x001914B0 File Offset: 0x0018F6B0
		public IMAPAuthenticationMechanism Authentication
		{
			get
			{
				return (IMAPAuthenticationMechanism)this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.Authentication];
			}
			set
			{
				this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.Authentication] = value;
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06005F10 RID: 24336 RVA: 0x001914C3 File Offset: 0x0018F6C3
		// (set) Token: 0x06005F11 RID: 24337 RVA: 0x001914D5 File Offset: 0x0018F6D5
		public IMAPSecurityMechanism Security
		{
			get
			{
				return (IMAPSecurityMechanism)this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.Security];
			}
			set
			{
				this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.Security] = value;
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x06005F12 RID: 24338 RVA: 0x001914E8 File Offset: 0x0018F6E8
		// (set) Token: 0x06005F13 RID: 24339 RVA: 0x001914FA File Offset: 0x0018F6FA
		public MultiValuedProperty<string> ExcludedFolders
		{
			get
			{
				return (MultiValuedProperty<string>)this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.ExcludedFolders];
			}
			set
			{
				this[IMAPConnectionSettings.IMAPConnectionSettingsSchema.ExcludedFolders] = value;
			}
		}

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x06005F14 RID: 24340 RVA: 0x00191508 File Offset: 0x0018F708
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return IMAPConnectionSettings.schema;
			}
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x00191510 File Offset: 0x0018F710
		public new static implicit operator IMAPConnectionSettings(string xml)
		{
			IMAPConnectionSettings result;
			try
			{
				IMAPConnectionSettings imapconnectionSettings = MigrationXmlSerializer.Deserialize<IMAPConnectionSettings>(xml);
				result = imapconnectionSettings;
			}
			catch (MigrationDataCorruptionException ex)
			{
				throw new CouldNotDeserializeConnectionSettingsException(ex.InnerException);
			}
			return result;
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x00191548 File Offset: 0x0018F748
		public override ConnectionSettingsBase CloneForPresentation()
		{
			return new IMAPConnectionSettings
			{
				Server = this.Server,
				Port = this.Port,
				Authentication = this.Authentication,
				Security = this.Security,
				ExcludedFolders = this.ExcludedFolders
			};
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x00191598 File Offset: 0x0018F798
		public override void ReadXml(XmlReader reader)
		{
			if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "IMAPConnectionSettings")
			{
				this.Server = reader["Server"];
				this.Port = int.Parse(reader["Port"]);
				this.Authentication = (IMAPAuthenticationMechanism)Enum.Parse(typeof(IMAPAuthenticationMechanism), reader["Authentication"]);
				this.Security = (IMAPSecurityMechanism)Enum.Parse(typeof(IMAPSecurityMechanism), reader["Security"]);
				while (reader.LocalName == "ExcludedFolders" || reader.ReadToFollowing("ExcludedFolders"))
				{
					string text = reader.ReadElementContentAsString();
					if (!string.IsNullOrEmpty(text))
					{
						this.ExcludedFolders.TryAdd(text);
					}
				}
			}
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x00191674 File Offset: 0x0018F874
		public override bool Equals(object obj)
		{
			IMAPConnectionSettings imapconnectionSettings = obj as IMAPConnectionSettings;
			return imapconnectionSettings != null && (this.Security == imapconnectionSettings.Security && StringComparer.InvariantCultureIgnoreCase.Equals(this.Server, imapconnectionSettings.Server) && this.Authentication == imapconnectionSettings.Authentication) && this.Port == imapconnectionSettings.Port;
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x001916D4 File Offset: 0x0018F8D4
		public override int GetHashCode()
		{
			return ((this.Server == null) ? 0 : this.Server.GetHashCode()) ^ this.Port ^ this.Security.GetHashCode() ^ this.Authentication.GetHashCode();
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x00191720 File Offset: 0x0018F920
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("IMAPConnectionSettings");
			writer.WriteAttributeString("Server", this.Server);
			writer.WriteAttributeString("Port", this.Port.ToString());
			writer.WriteAttributeString("Authentication", this.Authentication.ToString());
			writer.WriteAttributeString("Security", this.Security.ToString());
			foreach (string value in this.ExcludedFolders)
			{
				writer.WriteElementString("ExcludedFolders", value);
			}
			writer.WriteEndElement();
		}

		// Token: 0x040034E6 RID: 13542
		private const string RootSerializedTag = "IMAPConnectionSettings";

		// Token: 0x040034E7 RID: 13543
		private static ObjectSchema schema = ObjectSchema.GetInstance<IMAPConnectionSettings.IMAPConnectionSettingsSchema>();

		// Token: 0x02000A18 RID: 2584
		private class IMAPConnectionSettingsSchema : SimpleProviderObjectSchema
		{
			// Token: 0x040034E8 RID: 13544
			public static readonly SimpleProviderPropertyDefinition Server = new SimpleProviderPropertyDefinition("Server", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034E9 RID: 13545
			public static readonly SimpleProviderPropertyDefinition Port = new SimpleProviderPropertyDefinition("Port", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034EA RID: 13546
			public static readonly SimpleProviderPropertyDefinition Authentication = new SimpleProviderPropertyDefinition("IMAPAuthenticationMechanism", ExchangeObjectVersion.Exchange2010, typeof(IMAPAuthenticationMechanism), PropertyDefinitionFlags.TaskPopulated, IMAPAuthenticationMechanism.Basic, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034EB RID: 13547
			public static readonly SimpleProviderPropertyDefinition Security = new SimpleProviderPropertyDefinition("IMAPSecurityMechanism", ExchangeObjectVersion.Exchange2010, typeof(IMAPSecurityMechanism), PropertyDefinitionFlags.TaskPopulated, IMAPSecurityMechanism.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

			// Token: 0x040034EC RID: 13548
			public static readonly SimpleProviderPropertyDefinition ExcludedFolders = new SimpleProviderPropertyDefinition("ExcludedFolders", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
		}
	}
}
