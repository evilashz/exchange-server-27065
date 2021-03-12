using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.VariantConfiguration.Reflection;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x0200066A RID: 1642
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SettingOverrideSync : IDiagnosable
	{
		// Token: 0x06004CB3 RID: 19635 RVA: 0x0011B289 File Offset: 0x00119489
		private SettingOverrideSync()
		{
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06004CB4 RID: 19636 RVA: 0x0011B2B4 File Offset: 0x001194B4
		// (remove) Token: 0x06004CB5 RID: 19637 RVA: 0x0011B2EC File Offset: 0x001194EC
		public event EventHandler<RefreshCompletedEventArgs> RefreshCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06004CB6 RID: 19638 RVA: 0x0011B324 File Offset: 0x00119524
		// (remove) Token: 0x06004CB7 RID: 19639 RVA: 0x0011B35C File Offset: 0x0011955C
		public event EventHandler<RefreshFailedEventArgs> RefreshFailed;

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x06004CB8 RID: 19640 RVA: 0x0011B391 File Offset: 0x00119591
		public static SettingOverrideSync Instance
		{
			get
			{
				return SettingOverrideSync.instance;
			}
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x0011B400 File Offset: 0x00119600
		public void Start(bool refreshNow = true)
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).VariantConfig.SettingOverrideSync.Enabled && this.cache == null)
			{
				lock (this.instanceLock)
				{
					if (this.cache == null)
					{
						this.session = DirectorySessionFactory.NonCacheSessionFactory.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 341, "Start", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationSettings\\SettingOverrideSync.cs");
						this.cache = new ADObjectCache<SettingOverride, ConfigurationSettingsException>(new Func<SettingOverride[], SettingOverride[]>(this.Load), null);
						VariantConfiguration.UpdateCommitted += this.OnUpdateCommitted;
						using (ManualResetEvent waitForUpdate = new ManualResetEvent(false))
						{
							EventHandler<RefreshCompletedEventArgs> value = delegate(object sender, RefreshCompletedEventArgs args)
							{
								try
								{
									waitForUpdate.Set();
								}
								catch (ObjectDisposedException)
								{
								}
							};
							EventHandler<RefreshFailedEventArgs> value2 = delegate(object sender, RefreshFailedEventArgs args)
							{
								try
								{
									waitForUpdate.Set();
								}
								catch (ObjectDisposedException)
								{
								}
							};
							this.RefreshCompleted += value;
							this.RefreshFailed += value2;
							this.cache.Initialize(SettingOverrideSync.instance.RefreshInterval, refreshNow);
							VariantConfigurationOverride[] overrides = VariantConfiguration.Overrides;
							if (overrides != null && overrides.Length > 0 && refreshNow)
							{
								waitForUpdate.WaitOne(SettingOverrideSync.WaitForUpdateTimeout);
							}
							this.RefreshCompleted -= value;
							this.RefreshFailed -= value2;
						}
					}
				}
			}
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0011B59C File Offset: 0x0011979C
		public void Stop()
		{
			lock (this.instanceLock)
			{
				if (this.cache != null && this.cache.IsInitialized)
				{
					VariantConfiguration.UpdateCommitted -= this.OnUpdateCommitted;
					this.cache.Dispose();
					this.cache = null;
				}
			}
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0011B610 File Offset: 0x00119810
		public void Refresh()
		{
			ADObjectCache<SettingOverride, ConfigurationSettingsException> adobjectCache = null;
			lock (this.instanceLock)
			{
				if (this.cache != null && this.cache.IsInitialized)
				{
					adobjectCache = this.cache;
				}
			}
			if (adobjectCache != null)
			{
				adobjectCache.Refresh(null);
				return;
			}
			throw new InvalidOperationException("Setting override sync is not started.");
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0011B680 File Offset: 0x00119880
		public string GetDiagnosticComponentName()
		{
			return typeof(VariantConfiguration).Name;
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0011B694 File Offset: 0x00119894
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			SettingOverrideSync.SettingOverrideDiagnosableArgument settingOverrideDiagnosableArgument = new SettingOverrideSync.SettingOverrideDiagnosableArgument(parameters);
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			if (settingOverrideDiagnosableArgument.HasArgument("refresh"))
			{
				this.RefreshAndWait();
			}
			if (settingOverrideDiagnosableArgument.HasArgument("errors"))
			{
				xelement.Add(this.GetErrorsDiagnosticInfo());
			}
			else if (settingOverrideDiagnosableArgument.HasArgument("overrides"))
			{
				xelement.Add(this.GetOverridesDiagnosticInfo());
			}
			else if (settingOverrideDiagnosableArgument.HasArgument("refresh"))
			{
				xelement.Add(this.GetOverridesDiagnosticInfo());
			}
			else if (settingOverrideDiagnosableArgument.HasArgument("config"))
			{
				string content = null;
				VariantConfigurationSnapshot diagnosticInfoSnapshot = this.GetDiagnosticInfoSnapshot(settingOverrideDiagnosableArgument, out content);
				if (diagnosticInfoSnapshot != null)
				{
					xelement.Add(this.GetConstraintDiagnosticInfo(diagnosticInfoSnapshot));
					xelement.Add(this.GetConfigurationDiagnosticInfo(diagnosticInfoSnapshot));
				}
				else
				{
					xelement.Add(new XElement("Error", content));
				}
			}
			else
			{
				xelement.Add(new XElement("Help", "Allowed arguments: " + string.Join(", ", settingOverrideDiagnosableArgument.ArgumentSchema.Keys) + "."));
			}
			return xelement;
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0011B818 File Offset: 0x00119A18
		private void RefreshAndWait()
		{
			using (ManualResetEvent waitForUpdate = new ManualResetEvent(false))
			{
				EventHandler<RefreshCompletedEventArgs> value = delegate(object sender, RefreshCompletedEventArgs args)
				{
					try
					{
						waitForUpdate.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				};
				EventHandler<RefreshFailedEventArgs> value2 = delegate(object sender, RefreshFailedEventArgs args)
				{
					try
					{
						waitForUpdate.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				};
				this.RefreshCompleted += value;
				this.RefreshFailed += value2;
				this.Refresh();
				waitForUpdate.WaitOne(SettingOverrideSync.WaitForUpdateTimeout);
			}
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0011B8E8 File Offset: 0x00119AE8
		private VariantConfigurationSnapshot GetDiagnosticInfoSnapshot(SettingOverrideSync.SettingOverrideDiagnosableArgument argument, out string error)
		{
			IConstraintProvider constraintProvider = MachineSettingsContext.Local;
			if (argument.HasArgument("user"))
			{
				string userId = argument.GetArgument<string>("user");
				if (!string.IsNullOrWhiteSpace(userId))
				{
					string orgId = null;
					if (argument.HasArgument("org"))
					{
						orgId = argument.GetArgument<string>("org");
					}
					if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled && string.IsNullOrEmpty(orgId))
					{
						error = "Org is required.";
						return null;
					}
					ADUser user = null;
					ADNotificationAdapter.TryRunADOperation(delegate()
					{
						user = new SettingOverrideSync.UserResolver(userId, orgId).Resolve();
					}, 3);
					if (user == null)
					{
						error = "User not found.";
						return null;
					}
					constraintProvider = user.GetContext(null);
				}
			}
			error = null;
			return VariantConfiguration.GetSnapshot(constraintProvider, null, null);
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x0011B9E0 File Offset: 0x00119BE0
		private XElement GetErrorsDiagnosticInfo()
		{
			XElement xelement = new XElement("Errors");
			lock (this.errors)
			{
				foreach (SettingOverrideSync.DiagnosticsError diagnosticsError in this.errors)
				{
					xelement.Add(diagnosticsError.GetDiagnosticInfo());
				}
			}
			return xelement;
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x0011BA74 File Offset: 0x00119C74
		private XElement GetOverridesDiagnosticInfo()
		{
			XElement xelement = new XElement("Overrides", new XAttribute("Updated", (this.lastUpdate != null) ? this.lastUpdate.ToString() : string.Empty));
			IEnumerable<SettingOverrideSync.SettingOverrideDiagnosticInfo> enumerable = this.overridesInfo;
			if (enumerable != null)
			{
				foreach (SettingOverrideSync.SettingOverrideDiagnosticInfo settingOverrideDiagnosticInfo in enumerable)
				{
					xelement.Add(settingOverrideDiagnosticInfo.GetDiagnosticInfo());
				}
			}
			return xelement;
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0011BB10 File Offset: 0x00119D10
		private XElement GetConstraintDiagnosticInfo(VariantConfigurationSnapshot snapshot)
		{
			XElement xelement = new XElement("Constraints");
			foreach (KeyValuePair<string, string> keyValuePair in snapshot.Constraints)
			{
				XElement content = new XElement("Constraint", new object[]
				{
					keyValuePair.Value,
					new XAttribute("Name", keyValuePair.Key)
				});
				xelement.Add(content);
			}
			return xelement;
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0011BB9C File Offset: 0x00119D9C
		private XElement GetConfigurationDiagnosticInfo(VariantConfigurationSnapshot snapshot)
		{
			XElement xelement = new XElement("Configuration");
			bool enabled = snapshot.VariantConfig.InternalAccess.Enabled;
			foreach (string text in VariantConfiguration.Settings.GetComponents(enabled))
			{
				XElement xelement2 = new XElement(text);
				xelement.Add(xelement2);
				VariantConfigurationComponent variantConfigurationComponent = VariantConfiguration.Settings[text];
				foreach (string text2 in variantConfigurationComponent.GetSections(enabled))
				{
					XElement xelement3 = new XElement(text2);
					xelement2.Add(xelement3);
					ISettings @object = snapshot.GetObject<ISettings>(Path.GetFileName(variantConfigurationComponent.FileName), text2);
					foreach (Type type in new List<Type>(variantConfigurationComponent[text2].Type.GetInterfaces())
					{
						variantConfigurationComponent[text2].Type
					})
					{
						foreach (PropertyInfo propertyInfo in type.GetProperties())
						{
							if (!propertyInfo.Name.Equals("Name"))
							{
								XElement content = new XElement(propertyInfo.Name, this.GetConfigurationDiagnosticValueString(propertyInfo.GetValue(@object)));
								xelement3.Add(content);
							}
						}
					}
				}
			}
			return xelement;
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0011BD94 File Offset: 0x00119F94
		private string GetConfigurationDiagnosticValueString(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (value is IEnumerable<object>)
			{
				return string.Join<object>(",", (IEnumerable<object>)value);
			}
			return value.ToString();
		}

		// Token: 0x06004CC5 RID: 19653 RVA: 0x0011BDC0 File Offset: 0x00119FC0
		private void OnUpdateCommitted(object sender, UpdateCommittedEventArgs args)
		{
			this.lastUpdate = new ExDateTime?(ExDateTime.UtcNow);
			EventHandler<RefreshCompletedEventArgs> refreshCompleted = this.RefreshCompleted;
			if (refreshCompleted != null)
			{
				refreshCompleted(this, new RefreshCompletedEventArgs(true, VariantConfiguration.Overrides));
			}
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x06004CC6 RID: 19654 RVA: 0x0011BDFC File Offset: 0x00119FFC
		private TimeSpan RefreshInterval
		{
			get
			{
				TimeSpan result;
				try
				{
					result = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).VariantConfig.SettingOverrideSync.RefreshInterval;
				}
				catch (KeyNotFoundException)
				{
					result = SettingOverrideSync.DefaultRefreshInterval;
				}
				return result;
			}
		}

		// Token: 0x06004CC7 RID: 19655 RVA: 0x0011BE5C File Offset: 0x0011A05C
		private SettingOverride[] Load(SettingOverride[] existingValue)
		{
			try
			{
				IEnumerable<SettingOverrideSync.SettingOverrideDiagnosticInfo> source = this.ReadOverrides();
				VariantConfigurationOverride[] overrides = (from o in source
				where o.Status == SettingOverrideSync.OverrideStatus.Accepted
				select o.Override.GetVariantConfigurationOverride()).ToArray<VariantConfigurationOverride>();
				this.overridesInfo = source;
				if (VariantConfiguration.SetOverrides(overrides))
				{
					if (this.cache.RefreshInterval != this.RefreshInterval)
					{
						this.cache.SetRefreshInterval(this.RefreshInterval);
					}
				}
				else
				{
					EventHandler<RefreshCompletedEventArgs> refreshCompleted = this.RefreshCompleted;
					if (refreshCompleted != null)
					{
						refreshCompleted(this, new RefreshCompletedEventArgs(false, VariantConfiguration.Overrides));
					}
				}
				this.HandleLoadSuccess(overrides);
			}
			catch (ConfigurationSettingsADConfigDriverException e)
			{
				this.HandleLoadException(e);
				throw;
			}
			return null;
		}

		// Token: 0x06004CC8 RID: 19656 RVA: 0x0011BF78 File Offset: 0x0011A178
		private IEnumerable<SettingOverrideSync.SettingOverrideDiagnosticInfo> ReadOverrides()
		{
			ADOperationResult adoperationResult = null;
			List<SettingOverride> list = new List<SettingOverride>();
			List<SettingOverrideSync.SettingOverrideDiagnosticInfo> list2 = new List<SettingOverrideSync.SettingOverrideDiagnosticInfo>();
			try
			{
				bool[] array = new bool[]
				{
					default(bool),
					true
				};
				for (int i = 0; i < array.Length; i++)
				{
					SettingOverrideSync.<>c__DisplayClass1a CS$<>8__locals1 = new SettingOverrideSync.<>c__DisplayClass1a();
					CS$<>8__locals1.isFlight = array[i];
					SettingOverride[] adOverridesSubset = null;
					adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						adOverridesSubset = this.session.Find<SettingOverride>(SettingOverride.GetContainerId(CS$<>8__locals1.isFlight), QueryScope.OneLevel, null, null, int.MaxValue);
					}, 3);
					if (adOverridesSubset != null)
					{
						list.AddRange(adOverridesSubset);
					}
				}
			}
			catch (LocalizedException innerException)
			{
				throw new ConfigurationSettingsADConfigDriverException(innerException);
			}
			catch (InvalidOperationException innerException2)
			{
				throw new ConfigurationSettingsADConfigDriverException(innerException2);
			}
			if (!adoperationResult.Succeeded)
			{
				throw new ConfigurationSettingsADNotificationException(adoperationResult.Exception);
			}
			list.Sort(new Comparison<SettingOverride>(SettingOverrideSync.Compare));
			List<SettingOverrideException> list3 = new List<SettingOverrideException>();
			foreach (SettingOverride settingOverride in list)
			{
				if (!settingOverride.Applies)
				{
					list2.Add(new SettingOverrideSync.SettingOverrideDiagnosticInfo(settingOverride, SettingOverrideSync.OverrideStatus.NotApplicable, null));
				}
				else
				{
					try
					{
						SettingOverride.Validate(settingOverride.GetVariantConfigurationOverride(), true);
					}
					catch (SettingOverrideException ex)
					{
						list3.Add(ex);
						list2.Add(new SettingOverrideSync.SettingOverrideDiagnosticInfo(settingOverride, SettingOverrideSync.OverrideStatus.Invalid, ex));
						continue;
					}
					list2.Add(new SettingOverrideSync.SettingOverrideDiagnosticInfo(settingOverride, SettingOverrideSync.OverrideStatus.Accepted, null));
				}
			}
			this.HandleSettingOverrideException(list3);
			return list2;
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x0011C110 File Offset: 0x0011A310
		private static int Compare(SettingOverride override1, SettingOverride override2)
		{
			if (override1.WhenCreated == null && override2.WhenCreated != null)
			{
				return 1;
			}
			if (override1.WhenCreated != null && override2.WhenCreated == null)
			{
				return -1;
			}
			if (override1.WhenCreated != null && override2.WhenCreated != null)
			{
				if (override1.WhenCreated < override2.WhenCreated)
				{
					return 1;
				}
				if (override1.WhenCreated > override2.WhenCreated)
				{
					return -1;
				}
			}
			return string.Compare(override1.Id.Name, override2.Id.Name, true);
		}

		// Token: 0x06004CCA RID: 19658 RVA: 0x0011C218 File Offset: 0x0011A418
		private void HandleLoadException(Exception e)
		{
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_ConfigurationSettingsLoadError, base.GetType().Name, new object[]
			{
				e.ToString()
			});
			lock (this.errors)
			{
				while (this.errors.Count >= 50)
				{
					this.errors.Dequeue();
				}
				this.errors.Enqueue(new SettingOverrideSync.DiagnosticsError(e));
			}
			EventHandler<RefreshFailedEventArgs> refreshFailed = this.RefreshFailed;
			if (refreshFailed != null)
			{
				refreshFailed(this, new RefreshFailedEventArgs(e));
			}
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x0011C2C0 File Offset: 0x0011A4C0
		private void HandleLoadSuccess(VariantConfigurationOverride[] overrides)
		{
			lock (this.errors)
			{
				this.errors.Clear();
			}
		}

		// Token: 0x06004CCC RID: 19660 RVA: 0x0011C308 File Offset: 0x0011A508
		private void HandleSettingOverrideException(ICollection<SettingOverrideException> exceptions)
		{
			if (exceptions != null && exceptions.Count > 0)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_SettingOverrideValidationError, base.GetType().Name, new object[]
				{
					string.Join<SettingOverrideException>(Environment.NewLine, exceptions)
				});
			}
		}

		// Token: 0x04003470 RID: 13424
		private const int MaxErrorHistory = 50;

		// Token: 0x04003471 RID: 13425
		private static readonly TimeSpan DefaultRefreshInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04003472 RID: 13426
		private static readonly TimeSpan WaitForUpdateTimeout = TimeSpan.FromSeconds(5.0);

		// Token: 0x04003473 RID: 13427
		private readonly object instanceLock = new object();

		// Token: 0x04003474 RID: 13428
		private static SettingOverrideSync instance = new SettingOverrideSync();

		// Token: 0x04003475 RID: 13429
		private ADObjectCache<SettingOverride, ConfigurationSettingsException> cache;

		// Token: 0x04003476 RID: 13430
		private ExDateTime? lastUpdate = null;

		// Token: 0x04003477 RID: 13431
		private IConfigurationSession session;

		// Token: 0x04003478 RID: 13432
		private Queue<SettingOverrideSync.DiagnosticsError> errors = new Queue<SettingOverrideSync.DiagnosticsError>();

		// Token: 0x04003479 RID: 13433
		private IEnumerable<SettingOverrideSync.SettingOverrideDiagnosticInfo> overridesInfo;

		// Token: 0x0200066B RID: 1643
		private enum OverrideStatus
		{
			// Token: 0x0400347F RID: 13439
			NotApplicable,
			// Token: 0x04003480 RID: 13440
			Invalid,
			// Token: 0x04003481 RID: 13441
			Accepted
		}

		// Token: 0x0200066C RID: 1644
		private sealed class SettingOverrideDiagnosticInfo
		{
			// Token: 0x06004CD0 RID: 19664 RVA: 0x0011C37F File Offset: 0x0011A57F
			public SettingOverrideDiagnosticInfo(SettingOverride o, SettingOverrideSync.OverrideStatus status, SettingOverrideException e)
			{
				this.Override = o;
				this.Status = status;
				this.ValidationException = e;
			}

			// Token: 0x17001947 RID: 6471
			// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x0011C39C File Offset: 0x0011A59C
			// (set) Token: 0x06004CD2 RID: 19666 RVA: 0x0011C3A4 File Offset: 0x0011A5A4
			public SettingOverride Override { get; private set; }

			// Token: 0x17001948 RID: 6472
			// (get) Token: 0x06004CD3 RID: 19667 RVA: 0x0011C3AD File Offset: 0x0011A5AD
			// (set) Token: 0x06004CD4 RID: 19668 RVA: 0x0011C3B5 File Offset: 0x0011A5B5
			public SettingOverrideSync.OverrideStatus Status { get; private set; }

			// Token: 0x17001949 RID: 6473
			// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x0011C3BE File Offset: 0x0011A5BE
			// (set) Token: 0x06004CD6 RID: 19670 RVA: 0x0011C3C6 File Offset: 0x0011A5C6
			public SettingOverrideException ValidationException { get; private set; }

			// Token: 0x1700194A RID: 6474
			// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x0011C3D0 File Offset: 0x0011A5D0
			private string Message
			{
				get
				{
					switch (this.Status)
					{
					case SettingOverrideSync.OverrideStatus.NotApplicable:
						return string.Concat(new object[]
						{
							"Either this server version '",
							SettingOverrideXml.CurrentVersion,
							"' or its name '",
							Environment.MachineName,
							"' does not match criteria defined in the override."
						});
					case SettingOverrideSync.OverrideStatus.Invalid:
						if (this.ValidationException == null)
						{
							return "Unknown error during validation.";
						}
						return this.ValidationException.Message;
					case SettingOverrideSync.OverrideStatus.Accepted:
						return "This override has been accepted as applicable to this server.";
					default:
						return string.Empty;
					}
				}
			}

			// Token: 0x06004CD8 RID: 19672 RVA: 0x0011C454 File Offset: 0x0011A654
			public XElement GetDiagnosticInfo()
			{
				XElement xelement = new XElement((!string.IsNullOrEmpty(this.Override.FlightName)) ? "FlightOverride" : "SettingOverride");
				this.Add(xelement, "Name", this.Override.Name);
				this.Add(xelement, "Reason", this.Override.Reason);
				this.Add(xelement, "ModifiedBy", this.Override.ModifiedBy);
				this.Add(xelement, "FlightName", this.Override.FlightName);
				this.Add(xelement, "ComponentName", this.Override.ComponentName);
				this.Add(xelement, "SectionName", this.Override.SectionName);
				this.Add(xelement, "Status", this.Status.ToString());
				this.Add(xelement, "Message", this.Message);
				XElement xelement2 = new XElement("Parameters");
				xelement.Add(xelement2);
				foreach (string value in this.Override.Parameters)
				{
					this.Add(xelement2, "Parameter", value);
				}
				return xelement;
			}

			// Token: 0x06004CD9 RID: 19673 RVA: 0x0011C5AC File Offset: 0x0011A7AC
			private void Add(XElement parent, string element, string value)
			{
				if (value != null)
				{
					parent.Add(new XElement(element, value));
				}
			}

			// Token: 0x06004CDA RID: 19674 RVA: 0x0011C5C3 File Offset: 0x0011A7C3
			private void Add(XElement parent, string element, bool value)
			{
				this.Add(parent, element, value ? bool.TrueString : bool.FalseString);
			}
		}

		// Token: 0x0200066D RID: 1645
		private class DiagnosticsError
		{
			// Token: 0x06004CDB RID: 19675 RVA: 0x0011C5DC File Offset: 0x0011A7DC
			public DiagnosticsError(Exception e)
			{
				if (e == null)
				{
					throw new ArgumentNullException("e");
				}
				this.Exception = e;
				this.RaisedAt = DateTime.UtcNow;
			}

			// Token: 0x1700194B RID: 6475
			// (get) Token: 0x06004CDC RID: 19676 RVA: 0x0011C604 File Offset: 0x0011A804
			// (set) Token: 0x06004CDD RID: 19677 RVA: 0x0011C60C File Offset: 0x0011A80C
			public Exception Exception { get; private set; }

			// Token: 0x1700194C RID: 6476
			// (get) Token: 0x06004CDE RID: 19678 RVA: 0x0011C615 File Offset: 0x0011A815
			// (set) Token: 0x06004CDF RID: 19679 RVA: 0x0011C61D File Offset: 0x0011A81D
			public DateTime RaisedAt { get; private set; }

			// Token: 0x06004CE0 RID: 19680 RVA: 0x0011C628 File Offset: 0x0011A828
			public XElement GetDiagnosticInfo()
			{
				return new XElement("Error", new object[]
				{
					new XAttribute("RaisedAt", this.RaisedAt),
					new XElement("Exception", this.Exception)
				});
			}
		}

		// Token: 0x0200066E RID: 1646
		private sealed class SettingOverrideDiagnosableArgument : DiagnosableArgument
		{
			// Token: 0x06004CE1 RID: 19681 RVA: 0x0011C681 File Offset: 0x0011A881
			public SettingOverrideDiagnosableArgument(DiagnosableParameters parameters)
			{
				base.Initialize(parameters);
			}

			// Token: 0x1700194D RID: 6477
			// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x0011C690 File Offset: 0x0011A890
			public new Dictionary<string, Type> ArgumentSchema
			{
				get
				{
					return base.ArgumentSchema;
				}
			}

			// Token: 0x06004CE3 RID: 19683 RVA: 0x0011C698 File Offset: 0x0011A898
			protected override void InitializeSchema(Dictionary<string, Type> schema)
			{
				schema["errors"] = typeof(bool);
				schema["overrides"] = typeof(bool);
				schema["refresh"] = typeof(bool);
				schema["config"] = typeof(bool);
				schema["user"] = typeof(string);
				schema["org"] = typeof(string);
			}

			// Token: 0x04003487 RID: 13447
			public const string ErrorHistory = "errors";

			// Token: 0x04003488 RID: 13448
			public const string CurrentOverrides = "overrides";

			// Token: 0x04003489 RID: 13449
			public const string Refresh = "refresh";

			// Token: 0x0400348A RID: 13450
			public const string Configuration = "config";

			// Token: 0x0400348B RID: 13451
			public const string User = "user";

			// Token: 0x0400348C RID: 13452
			public const string Organization = "org";
		}

		// Token: 0x0200066F RID: 1647
		private class UserResolver
		{
			// Token: 0x06004CE4 RID: 19684 RVA: 0x0011C724 File Offset: 0x0011A924
			public UserResolver(string userId, string orgId)
			{
				this.userId = userId;
				ADSessionSettings sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(orgId);
				this.session = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 1127, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationSettings\\SettingOverrideSync.cs");
			}

			// Token: 0x06004CE5 RID: 19685 RVA: 0x0011C76C File Offset: 0x0011A96C
			public ADUser Resolve()
			{
				Func<ADUser>[] array = new Func<ADUser>[]
				{
					new Func<ADUser>(this.TryAccountName),
					new Func<ADUser>(this.TryObjectId),
					new Func<ADUser>(this.TryExternalDirectoryId),
					new Func<ADUser>(this.TryGuid),
					new Func<ADUser>(this.TryLegacyDN),
					new Func<ADUser>(this.TryProxyAddress),
					new Func<ADUser>(this.TrySid)
				};
				foreach (Func<ADUser> func in array)
				{
					ADUser aduser = func();
					if (aduser != null)
					{
						return aduser;
					}
				}
				return null;
			}

			// Token: 0x06004CE6 RID: 19686 RVA: 0x0011C824 File Offset: 0x0011AA24
			private ADUser TryAccountName()
			{
				IEnumerable<ADUser> enumerable = this.session.FindByAccountName<ADUser>(null, this.userId, null, null);
				if (enumerable == null)
				{
					return null;
				}
				return enumerable.FirstOrDefault<ADUser>();
			}

			// Token: 0x06004CE7 RID: 19687 RVA: 0x0011C854 File Offset: 0x0011AA54
			private ADUser TryObjectId()
			{
				ADObjectId adObjectId = null;
				if (ADObjectId.TryParseDnOrGuid(this.userId, out adObjectId))
				{
					return this.session.FindADUserByObjectId(adObjectId);
				}
				return null;
			}

			// Token: 0x06004CE8 RID: 19688 RVA: 0x0011C880 File Offset: 0x0011AA80
			private ADUser TryExternalDirectoryId()
			{
				return this.session.FindADUserByExternalDirectoryObjectId(this.userId);
			}

			// Token: 0x06004CE9 RID: 19689 RVA: 0x0011C894 File Offset: 0x0011AA94
			private ADUser TryGuid()
			{
				Guid guid;
				if (!Guid.TryParse(this.userId, out guid))
				{
					return null;
				}
				ADUser aduser = this.ConvertRecipientToUser(this.session.FindByExchangeGuidIncludingAlternate(guid));
				if (aduser == null)
				{
					aduser = this.ConvertRecipientToUser(this.session.FindByExchangeObjectId(guid));
				}
				if (aduser == null)
				{
					aduser = this.ConvertRecipientToUser(this.session.FindByObjectGuid(guid));
				}
				return aduser;
			}

			// Token: 0x06004CEA RID: 19690 RVA: 0x0011C8F2 File Offset: 0x0011AAF2
			private ADUser TryLegacyDN()
			{
				return this.ConvertRecipientToUser(this.session.FindByLegacyExchangeDN(this.userId));
			}

			// Token: 0x06004CEB RID: 19691 RVA: 0x0011C90C File Offset: 0x0011AB0C
			private ADUser TryProxyAddress()
			{
				ProxyAddress proxyAddress;
				if (ProxyAddress.TryParse(this.userId, out proxyAddress))
				{
					return this.ConvertRecipientToUser(this.session.FindByProxyAddress(proxyAddress));
				}
				return null;
			}

			// Token: 0x06004CEC RID: 19692 RVA: 0x0011C93C File Offset: 0x0011AB3C
			private ADUser TrySid()
			{
				try
				{
					SecurityIdentifier sId = new SecurityIdentifier(this.userId);
					return this.ConvertRecipientToUser(this.session.FindBySid(sId));
				}
				catch
				{
				}
				return null;
			}

			// Token: 0x06004CED RID: 19693 RVA: 0x0011C980 File Offset: 0x0011AB80
			private ADUser ConvertRecipientToUser(IEnumerable<ADRecipient> recipient)
			{
				if (recipient == null)
				{
					return null;
				}
				return this.ConvertRecipientToUser(recipient.FirstOrDefault<ADRecipient>());
			}

			// Token: 0x06004CEE RID: 19694 RVA: 0x0011C993 File Offset: 0x0011AB93
			private ADUser ConvertRecipientToUser(ADRecipient recipient)
			{
				if (recipient == null)
				{
					return null;
				}
				return this.session.FindADUserByObjectId(recipient.Id);
			}

			// Token: 0x0400348D RID: 13453
			private string userId;

			// Token: 0x0400348E RID: 13454
			private IRecipientSession session;
		}
	}
}
