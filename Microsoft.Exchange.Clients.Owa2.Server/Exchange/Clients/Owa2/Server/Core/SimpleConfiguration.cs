using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000EB RID: 235
	internal class SimpleConfiguration<T> where T : new()
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x0001BD94 File Offset: 0x00019F94
		internal SimpleConfiguration()
		{
			Type typeFromHandle = typeof(T);
			if (!SimpleConfiguration<T>.simpleConfigurationTable.ContainsKey(typeFromHandle))
			{
				this.AddConfiguration(typeFromHandle);
			}
			this.configurationAttribute = SimpleConfiguration<T>.simpleConfigurationTable[typeFromHandle];
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0001BDE2 File Offset: 0x00019FE2
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x0001BDEA File Offset: 0x00019FEA
		public IList<T> Entries
		{
			get
			{
				return this.entries;
			}
			set
			{
				this.entries = value;
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public void Load(CallContext callContext)
		{
			this.entries.Clear();
			using (UserConfiguration userConfiguration = this.GetUserConfiguration(this.configurationAttribute.ConfigurationName, callContext.SessionCache.GetMailboxIdentityMailboxSession()))
			{
				using (Stream xmlStream = userConfiguration.GetXmlStream())
				{
					if (xmlStream != null && xmlStream.Length > 0L)
					{
						this.reader = SafeXmlFactory.CreateSafeXmlTextReader(xmlStream);
						this.Parse(this.reader, callContext);
					}
				}
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001BE8C File Offset: 0x0001A08C
		public void Save(CallContext callContext)
		{
			if (callContext.SessionCache == null)
			{
				throw new InvalidOperationException("We cannot get the MailboxSession from the given callContext. Please make sure the callContext is not disposed when this method is called.");
			}
			this.Save(callContext.SessionCache.GetMailboxIdentityMailboxSession());
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001BEB4 File Offset: 0x0001A0B4
		public void Save(MailboxSession mailboxSession)
		{
			using (UserConfiguration userConfiguration = this.GetUserConfiguration(this.configurationAttribute.ConfigurationName, mailboxSession))
			{
				using (Stream xmlStream = userConfiguration.GetXmlStream())
				{
					xmlStream.SetLength(0L);
					using (StreamWriter streamWriter = PendingRequestUtilities.CreateStreamWriter(xmlStream))
					{
						using (XmlTextWriter xmlTextWriter = new XmlTextWriter(streamWriter))
						{
							xmlTextWriter.WriteStartElement(this.configurationAttribute.ConfigurationRootNodeName);
							foreach (T t in this.entries)
							{
								SimpleConfigurationAttribute simpleConfigurationAttribute;
								lock (SimpleConfiguration<T>.simpleConfigurationTable)
								{
									simpleConfigurationAttribute = SimpleConfiguration<T>.simpleConfigurationTable[t.GetType()];
								}
								xmlTextWriter.WriteStartElement("entry");
								this.WriteCustomAttributes(xmlTextWriter, t);
								foreach (SimpleConfigurationPropertyAttribute simpleConfigurationPropertyAttribute in simpleConfigurationAttribute.GetPropertyCollection())
								{
									object value = simpleConfigurationPropertyAttribute.GetValue(t);
									if (value != null)
									{
										xmlTextWriter.WriteAttributeString(simpleConfigurationPropertyAttribute.Name, value.ToString());
									}
								}
								xmlTextWriter.WriteFullEndElement();
							}
							xmlTextWriter.WriteFullEndElement();
						}
					}
				}
				this.TrySaveConfiguration(userConfiguration, true);
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001C0CC File Offset: 0x0001A2CC
		protected virtual void WriteCustomAttributes(XmlTextWriter xmlWriter, T entry)
		{
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		protected virtual void AddConfiguration(Type configurationType)
		{
			lock (SimpleConfiguration<T>.simpleConfigurationTable)
			{
				if (!SimpleConfiguration<T>.simpleConfigurationTable.ContainsKey(configurationType))
				{
					object[] customAttributes = configurationType.GetCustomAttributes(typeof(SimpleConfigurationAttribute), false);
					if (customAttributes == null || customAttributes.Length == 0)
					{
						throw new OwaNotSupportedException("A SimpleConfigurationAttribute should be defined on the type");
					}
					SimpleConfigurationAttribute simpleConfigurationAttribute = (SimpleConfigurationAttribute)customAttributes[0];
					PropertyInfo[] properties = configurationType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
					foreach (PropertyInfo propertyInfo in properties)
					{
						object[] customAttributes2 = propertyInfo.GetCustomAttributes(typeof(SimpleConfigurationPropertyAttribute), false);
						if (customAttributes2 != null && customAttributes2.Length != 0)
						{
							SimpleConfigurationPropertyAttribute simpleConfigurationPropertyAttribute = (SimpleConfigurationPropertyAttribute)customAttributes2[0];
							simpleConfigurationPropertyAttribute.PropertyInfo = propertyInfo;
							simpleConfigurationAttribute.AddProperty(simpleConfigurationPropertyAttribute);
						}
					}
					SimpleConfiguration<T>.simpleConfigurationTable.Add(configurationType, simpleConfigurationAttribute);
				}
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		protected virtual T ParseEntry(XmlTextReader reader)
		{
			Dictionary<string, string> attributes = this.ParseAttributes(reader);
			T t = this.CreateObject(attributes);
			this.SetAttributeValues(attributes, t);
			return t;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
		protected virtual T CreateObject(Dictionary<string, string> attributes)
		{
			if (default(T) != null)
			{
				return default(T);
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001C210 File Offset: 0x0001A410
		private UserConfiguration GetUserConfiguration(string configurationName, MailboxSession mailboxSession)
		{
			if (string.IsNullOrEmpty(configurationName))
			{
				throw new ArgumentException("configurationName must not be null or empty");
			}
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = mailboxSession.UserConfigurationManager.GetMailboxConfiguration(configurationName, UserConfigurationTypes.XML);
			}
			catch (ObjectNotFoundException)
			{
				userConfiguration = mailboxSession.UserConfigurationManager.CreateMailboxConfiguration(configurationName, UserConfigurationTypes.XML);
				try
				{
					this.TrySaveConfiguration(userConfiguration, false);
				}
				catch (ObjectExistedException)
				{
					try
					{
						userConfiguration = mailboxSession.UserConfigurationManager.GetMailboxConfiguration(configurationName, UserConfigurationTypes.XML);
					}
					catch (ObjectNotFoundException thisObject)
					{
						throw new OwaSaveConflictException("A save conflict happened during the creation and save of the userconfiguration.", thisObject);
					}
				}
				catch (StoragePermanentException)
				{
				}
			}
			return userConfiguration;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
		private void TrySaveConfiguration(UserConfiguration configuration, bool ignoreStorePermanentExceptions)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			bool flag = false;
			bool flag2 = false;
			Exception ex = null;
			try
			{
				configuration.Save();
			}
			catch (StoragePermanentException ex2)
			{
				flag = true;
				ex = ex2;
				flag2 = true;
			}
			catch (StorageTransientException ex3)
			{
				flag = true;
				ex = ex3;
			}
			if (flag)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "UserConfigurationUtilities.TrySaveConfiguration: Failed. Exception: {0}", ex.Message);
				if (!ignoreStorePermanentExceptions && flag2)
				{
					throw ex;
				}
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001C334 File Offset: 0x0001A534
		private void Parse(XmlTextReader reader, CallContext callContext)
		{
			try
			{
				reader.WhitespaceHandling = WhitespaceHandling.All;
				this.state = SimpleConfiguration<T>.XmlParseState.Start;
				while (this.state != SimpleConfiguration<T>.XmlParseState.Finished && reader.Read())
				{
					switch (this.state)
					{
					case SimpleConfiguration<T>.XmlParseState.Start:
						this.ParseStart(reader);
						break;
					case SimpleConfiguration<T>.XmlParseState.Root:
						this.ParseRoot(reader);
						break;
					case SimpleConfiguration<T>.XmlParseState.Child:
						this.ParseChild(reader);
						break;
					}
				}
			}
			catch (XmlException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Parser threw an XML exception: {0}'", ex.Message);
				this.entries.Clear();
				this.Save(callContext);
			}
			catch (OwaConfigurationParserException ex2)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Mru parser threw an exception: {0}'", ex2.Message);
				this.entries.Clear();
				this.Save(callContext);
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001C40C File Offset: 0x0001A60C
		private void ParseStart(XmlTextReader reader)
		{
			if (XmlNodeType.Element != reader.NodeType || string.CompareOrdinal(this.configurationAttribute.ConfigurationRootNodeName, reader.Name) != 0)
			{
				this.ThrowParserException();
				return;
			}
			if (reader.IsEmptyElement)
			{
				this.state = SimpleConfiguration<T>.XmlParseState.Finished;
				return;
			}
			this.state = SimpleConfiguration<T>.XmlParseState.Root;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001C458 File Offset: 0x0001A658
		private void ParseRoot(XmlTextReader reader)
		{
			if (reader.NodeType == XmlNodeType.Element)
			{
				if (reader.IsEmptyElement)
				{
					this.ThrowParserException();
					return;
				}
				if (string.CompareOrdinal("entry", reader.Name) == 0)
				{
					T item = this.ParseEntry(reader);
					this.entries.Add(item);
					this.state = SimpleConfiguration<T>.XmlParseState.Child;
					return;
				}
				this.ThrowParserException();
				return;
			}
			else
			{
				if (reader.NodeType == XmlNodeType.EndElement && string.CompareOrdinal(this.configurationAttribute.ConfigurationRootNodeName, reader.Name) == 0)
				{
					this.state = SimpleConfiguration<T>.XmlParseState.Finished;
					return;
				}
				this.ThrowParserException();
				return;
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001C4E2 File Offset: 0x0001A6E2
		private void ParseChild(XmlTextReader reader)
		{
			if (reader.NodeType == XmlNodeType.EndElement && string.CompareOrdinal(reader.Name, "entry") == 0)
			{
				this.state = SimpleConfiguration<T>.XmlParseState.Root;
				return;
			}
			this.ThrowParserException();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001C510 File Offset: 0x0001A710
		private Dictionary<string, string> ParseAttributes(XmlTextReader reader)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(reader.AttributeCount);
			if (reader.HasAttributes)
			{
				for (int i = 0; i < reader.AttributeCount; i++)
				{
					reader.MoveToAttribute(i);
					dictionary[reader.Name] = reader.Value;
				}
				reader.MoveToElement();
			}
			return dictionary;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001C564 File Offset: 0x0001A764
		private void SetAttributeValues(Dictionary<string, string> attributes, object entry)
		{
			SimpleConfigurationAttribute simpleConfigurationAttribute;
			lock (SimpleConfiguration<T>.simpleConfigurationTable)
			{
				simpleConfigurationAttribute = SimpleConfiguration<T>.simpleConfigurationTable[entry.GetType()];
			}
			ulong num = simpleConfigurationAttribute.RequiredMask;
			foreach (KeyValuePair<string, string> keyValuePair in attributes)
			{
				SimpleConfigurationPropertyAttribute simpleConfigurationPropertyAttribute = simpleConfigurationAttribute.TryGetProperty(keyValuePair.Key);
				if (simpleConfigurationPropertyAttribute == null)
				{
					this.ThrowParserException();
				}
				object value = this.ConvertToStrongType(simpleConfigurationPropertyAttribute.Type, keyValuePair.Value);
				simpleConfigurationPropertyAttribute.SetValue(entry, value);
				if (simpleConfigurationPropertyAttribute.IsRequired)
				{
					num &= ~simpleConfigurationPropertyAttribute.PropertyMask;
				}
			}
			if (num != 0UL)
			{
				this.ThrowParserException();
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001C644 File Offset: 0x0001A844
		private object ConvertToStrongType(Type type, string value)
		{
			try
			{
				if (type == typeof(string))
				{
					return value;
				}
				if (type == typeof(int))
				{
					return int.Parse(value, CultureInfo.InvariantCulture);
				}
				if (type == typeof(long))
				{
					return long.Parse(value, CultureInfo.InvariantCulture);
				}
				if (type == typeof(double))
				{
					return double.Parse(value, CultureInfo.InvariantCulture);
				}
				if (type == typeof(ExDateTime))
				{
					return new ExDateTime(ExTimeZone.CurrentTimeZone, Convert.ToDateTime(value));
				}
				if (type == typeof(DateTime))
				{
					return Convert.ToDateTime(value);
				}
				if (type == typeof(bool))
				{
					return Convert.ToBoolean(value);
				}
				if (type.IsEnum)
				{
					return Enum.Parse(type, value);
				}
				this.ThrowParserException(string.Format("Internal error: unsupported type : {0}", type));
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			this.ThrowParserException(string.Format(CultureInfo.InvariantCulture, "Failed to parse type. Type = {0}, Value = {1}", new object[]
			{
				type,
				value
			}));
			return null;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001C7D8 File Offset: 0x0001A9D8
		private void ThrowParserException()
		{
			this.ThrowParserException(null);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
		private void ThrowParserException(string description)
		{
			int num = 0;
			int num2 = 0;
			if (this.reader != null)
			{
				num = this.reader.LineNumber;
				num2 = this.reader.LinePosition;
			}
			throw new OwaConfigurationParserException(string.Format(CultureInfo.InvariantCulture, "Invalid simple configuration. Line number: {0} Position: {1}.{2}", new object[]
			{
				num.ToString(CultureInfo.InvariantCulture),
				num2.ToString(CultureInfo.InvariantCulture),
				(description != null) ? (" " + description) : string.Empty
			}), null, this);
		}

		// Token: 0x0400053A RID: 1338
		private const string EntryNodeName = "entry";

		// Token: 0x0400053B RID: 1339
		private static Dictionary<Type, SimpleConfigurationAttribute> simpleConfigurationTable = new Dictionary<Type, SimpleConfigurationAttribute>();

		// Token: 0x0400053C RID: 1340
		private IList<T> entries = new List<T>();

		// Token: 0x0400053D RID: 1341
		private SimpleConfigurationAttribute configurationAttribute;

		// Token: 0x0400053E RID: 1342
		private SimpleConfiguration<T>.XmlParseState state;

		// Token: 0x0400053F RID: 1343
		private XmlTextReader reader;

		// Token: 0x020000EC RID: 236
		private enum XmlParseState
		{
			// Token: 0x04000541 RID: 1345
			Start,
			// Token: 0x04000542 RID: 1346
			Root,
			// Token: 0x04000543 RID: 1347
			Child,
			// Token: 0x04000544 RID: 1348
			Finished
		}
	}
}
