using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003DE RID: 990
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class UserConfigurationBaseType
	{
		// Token: 0x06001FB1 RID: 8113 RVA: 0x00077902 File Offset: 0x00075B02
		public UserConfigurationBaseType(string configurationName)
		{
			this.configurationName = configurationName;
			this.accessStrategy = this.CreateConfigurationAccessStrategy();
			this.Init();
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001FB2 RID: 8114
		internal abstract UserConfigurationPropertySchemaBase Schema { get; }

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00077923 File Offset: 0x00075B23
		internal virtual IUserConfigurationAccessStrategy CreateConfigurationAccessStrategy()
		{
			return new MailboxConfigurationAccessStrategy();
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0007792A File Offset: 0x00075B2A
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x00077932 File Offset: 0x00075B32
		protected Dictionary<UserConfigurationPropertyDefinition, object> OptionProperties
		{
			get
			{
				return this.optionProperties;
			}
			set
			{
				this.optionProperties = value;
			}
		}

		// Token: 0x17000788 RID: 1928
		protected object this[UserConfigurationPropertyId propertyID]
		{
			get
			{
				UserConfigurationPropertyDefinition propertyDefinition = this.Schema.GetPropertyDefinition(propertyID);
				object obj;
				if (this.OptionProperties.ContainsKey(propertyDefinition) && this.OptionProperties[propertyDefinition] != null)
				{
					obj = this.OptionProperties[propertyDefinition];
				}
				else
				{
					obj = propertyDefinition.GetValidatedProperty(null);
				}
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, object>((long)this.GetHashCode(), "Get property: '{0}'; value: '{1}'", propertyDefinition.PropertyName, obj);
				return obj;
			}
			set
			{
				UserConfigurationPropertyDefinition propertyDefinition = this.Schema.GetPropertyDefinition(propertyID);
				object value2 = propertyDefinition.GetValidatedProperty(value);
				if (!this.OptionProperties.ContainsKey(propertyDefinition))
				{
					this.OptionProperties.Add(propertyDefinition, value2);
				}
				else
				{
					this.OptionProperties[propertyDefinition] = value2;
				}
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, object>((long)this.GetHashCode(), "Set property: '{0}'; value: '{1}'", propertyDefinition.PropertyName, this.OptionProperties[propertyDefinition]);
			}
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00077A25 File Offset: 0x00075C25
		internal void Load(MailboxSession mailboxSession, IList<UserConfigurationPropertyDefinition> properties)
		{
			this.Load(mailboxSession, properties, false);
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x00077A30 File Offset: 0x00075C30
		internal void Load(MailboxSession mailboxSession, IList<UserConfigurationPropertyDefinition> properties, bool ignoreOverQuotaException)
		{
			try
			{
				using (IReadableUserConfiguration readOnlyConfiguration = this.GetReadOnlyConfiguration(mailboxSession))
				{
					if (readOnlyConfiguration != null)
					{
						IDictionary dictionary = readOnlyConfiguration.GetDictionary();
						for (int i = 0; i < properties.Count; i++)
						{
							UserConfigurationPropertyDefinition userConfigurationPropertyDefinition = properties[i];
							object originalValue = dictionary[userConfigurationPropertyDefinition.PropertyName];
							this.optionProperties[userConfigurationPropertyDefinition] = userConfigurationPropertyDefinition.GetValidatedProperty(originalValue);
							ExTraceGlobals.UserOptionsDataTracer.TraceDebug((long)this.GetHashCode(), "Loaded property: {0}", new object[]
							{
								this.optionProperties[userConfigurationPropertyDefinition]
							});
						}
					}
				}
			}
			catch (QuotaExceededException ex)
			{
				ExTraceGlobals.UserContextCallTracer.TraceDebug<string>(0L, "UserConfigurationBaseType: Load failed. Exception: {0}", ex.Message);
				if (!ignoreOverQuotaException)
				{
					throw;
				}
			}
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00077B0C File Offset: 0x00075D0C
		internal void Load(CallContext callContext, IList<UserConfigurationPropertyDefinition> properties)
		{
			MailboxSession mailboxIdentityMailboxSession = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.Load(mailboxIdentityMailboxSession, properties, false);
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x00077B30 File Offset: 0x00075D30
		internal void Load(CallContext callContext, IList<UserConfigurationPropertyDefinition> properties, bool ignoreOverQuotaException)
		{
			MailboxSession mailboxIdentityMailboxSession = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.Load(mailboxIdentityMailboxSession, properties, ignoreOverQuotaException);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x00077B54 File Offset: 0x00075D54
		internal void Commit(CallContext callContext, IList<UserConfigurationPropertyDefinition> properties)
		{
			MailboxSession mailboxIdentityMailboxSession = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.Commit(mailboxIdentityMailboxSession, properties);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00077B78 File Offset: 0x00075D78
		internal void Commit(MailboxSession mailboxSession, IList<UserConfigurationPropertyDefinition> properties)
		{
			using (IUserConfiguration configuration = this.GetConfiguration(mailboxSession))
			{
				IDictionary dictionary = configuration.GetDictionary();
				Type typeFromHandle = typeof(int);
				for (int i = 0; i < properties.Count; i++)
				{
					UserConfigurationPropertyDefinition userConfigurationPropertyDefinition = properties[i];
					string propertyName = userConfigurationPropertyDefinition.PropertyName;
					if (userConfigurationPropertyDefinition.PropertyType == typeFromHandle)
					{
						dictionary[userConfigurationPropertyDefinition.PropertyName] = (int)this.optionProperties[userConfigurationPropertyDefinition];
					}
					else
					{
						dictionary[userConfigurationPropertyDefinition.PropertyName] = this.optionProperties[userConfigurationPropertyDefinition];
					}
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug((long)this.GetHashCode(), "Committed property: {0}", new object[]
					{
						this.optionProperties[userConfigurationPropertyDefinition]
					});
				}
				try
				{
					configuration.Save();
				}
				catch (StoragePermanentException ex)
				{
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Failed to save configuration data. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
					throw;
				}
				catch (StorageTransientException ex2)
				{
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Failed to save configuration data. Error: {0}. Stack: {1}.", ex2.Message, ex2.StackTrace);
					throw;
				}
			}
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x00077CF8 File Offset: 0x00075EF8
		private IReadableUserConfiguration GetReadOnlyConfiguration(MailboxSession session)
		{
			return this.InternalGetConfiguration<IReadableUserConfiguration>(session, new Func<MailboxSession, string, UserConfigurationTypes, IReadableUserConfiguration>(this.accessStrategy.GetReadOnlyConfiguration), new Func<MailboxSession, bool, IReadableUserConfiguration>(this.RecreateUserConfiguration));
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00077D1F File Offset: 0x00075F1F
		private IUserConfiguration GetConfiguration(MailboxSession session)
		{
			return this.InternalGetConfiguration<IUserConfiguration>(session, new Func<MailboxSession, string, UserConfigurationTypes, IUserConfiguration>(this.accessStrategy.GetConfiguration), new Func<MailboxSession, bool, IUserConfiguration>(this.RecreateUserConfiguration));
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x00077D48 File Offset: 0x00075F48
		private T InternalGetConfiguration<T>(MailboxSession mailboxSession, Func<MailboxSession, string, UserConfigurationTypes, T> getter, Func<MailboxSession, bool, T> recreator) where T : class, IReadableUserConfiguration
		{
			T t = default(T);
			try
			{
				t = getter(mailboxSession, this.configurationName, UserConfigurationTypes.Dictionary);
			}
			catch (AccessDeniedException ex)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceError<string, string, string>((long)this.GetHashCode(), "Logon user does not have access permissions to user configuration object. Configuration: {0}. Error: {1}. Stack: {2}.", this.configurationName, ex.Message, ex.StackTrace);
				throw;
			}
			catch (InvalidDataException ex2)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Recreating user configuration due to invalid data. Configuration: {0}. Error: {1}. Stack: {2}.", this.configurationName, ex2.Message, ex2.StackTrace);
				return recreator(mailboxSession, true);
			}
			catch (CorruptDataException ex3)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Recreating user configuration due to invalid data. Configuration: {0}. Error: {1}. Stack: {2}.", this.configurationName, ex3.Message, ex3.StackTrace);
				return recreator(mailboxSession, true);
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Creating user configuration because it does not exist. Configuration: {0}", this.configurationName);
				return recreator(mailboxSession, false);
			}
			bool flag = false;
			T result;
			try
			{
				t.GetDictionary();
				flag = true;
				result = t;
			}
			catch (InvalidOperationException ex4)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Recreating user configuration due to no dictionary in UserConfiguration. Configuration: {0}. Error: {1}. Stack: {2}.", this.configurationName, ex4.Message, ex4.StackTrace);
				result = recreator(mailboxSession, true);
			}
			catch (InvalidDataException ex5)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Recreating user configuration due corrupt dictionary. Configuration: {0}. Error: {1}. Stack: {2}.", this.configurationName, ex5.Message, ex5.StackTrace);
				result = recreator(mailboxSession, true);
			}
			catch (CorruptDataException ex6)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Recreating user configuration due to invalid data. Configuration: {0}. Error: {1}. Stack: {2}.", this.configurationName, ex6.Message, ex6.StackTrace);
				result = recreator(mailboxSession, true);
			}
			finally
			{
				if (!flag && t != null)
				{
					t.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00077F80 File Offset: 0x00076180
		private IUserConfiguration RecreateUserConfiguration(MailboxSession mailboxSession, bool deleteFirst)
		{
			IUserConfiguration userConfiguration = null;
			bool flag = false;
			IUserConfiguration result;
			try
			{
				if (deleteFirst)
				{
					this.accessStrategy.DeleteConfigurations(mailboxSession, new string[]
					{
						this.configurationName
					});
				}
				userConfiguration = this.accessStrategy.CreateConfiguration(mailboxSession, this.configurationName, UserConfigurationTypes.Dictionary);
				userConfiguration.Save();
				flag = true;
				result = userConfiguration;
			}
			catch (ObjectExistedException ex)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceWarning<string, string>((long)this.GetHashCode(), "User configuration data already created. Error: {0}. Stack: {1}.", ex.Message, ex.StackTrace);
				if (userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
				userConfiguration = this.accessStrategy.GetConfiguration(mailboxSession, this.configurationName, UserConfigurationTypes.Dictionary);
				flag = true;
				result = userConfiguration;
			}
			catch (StoragePermanentException ex2)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to recreate configuration data. Error: {0}. Stack: {1}.", ex2.Message, ex2.StackTrace);
				throw;
			}
			catch (StorageTransientException ex3)
			{
				ExTraceGlobals.UserOptionsDataTracer.TraceError<string, string>((long)this.GetHashCode(), "Failed to recreate configuration data. Error: {0}. Stack: {1}.", ex3.Message, ex3.StackTrace);
				throw;
			}
			finally
			{
				if (!flag && userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000780B0 File Offset: 0x000762B0
		[OnDeserializing]
		private void SetValuesOnDeserializing(StreamingContext streamingContext)
		{
			this.Init();
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000780B8 File Offset: 0x000762B8
		private void Init()
		{
			this.CreateProperties();
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x000780C0 File Offset: 0x000762C0
		private void CreateProperties()
		{
			this.OptionProperties = new Dictionary<UserConfigurationPropertyDefinition, object>();
			for (int i = 0; i < this.Schema.Count; i++)
			{
				this.OptionProperties.Add(this.Schema.GetPropertyDefinition(i), null);
			}
		}

		// Token: 0x04001205 RID: 4613
		private readonly string configurationName;

		// Token: 0x04001206 RID: 4614
		private Dictionary<UserConfigurationPropertyDefinition, object> optionProperties;

		// Token: 0x04001207 RID: 4615
		private IUserConfigurationAccessStrategy accessStrategy;
	}
}
