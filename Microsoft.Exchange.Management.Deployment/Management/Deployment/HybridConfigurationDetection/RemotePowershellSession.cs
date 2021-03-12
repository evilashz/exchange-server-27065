using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Hybrid;

namespace Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection
{
	// Token: 0x02000037 RID: 55
	internal class RemotePowershellSession : IDisposable
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00004FD9 File Offset: 0x000031D9
		public RemotePowershellSession(string targetServer, PowershellConnectionType connectionType, bool useSSL, ILogger logger)
		{
			this.targetServer = targetServer;
			this.connectionType = connectionType;
			this.useSSL = useSSL;
			if (logger == null)
			{
				throw new ArgumentNullException();
			}
			this.logger = logger;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005008 File Offset: 0x00003208
		public void Dispose()
		{
			if (this.isDisposed)
			{
				return;
			}
			if (this.runspace != null)
			{
				if (this.openedRunspace)
				{
					this.runspace.Close();
				}
				this.runspace.Dispose();
				this.logger.Log(Strings.HybridInfoTotalCmdletTime(this.connectionType.ToString(), this.totalProcessedTime.TotalSeconds));
			}
			this.isDisposed = true;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005078 File Offset: 0x00003278
		public void Connect(PSCredential credentials, CultureInfo sessionUiCulture)
		{
			this.CheckDisposed();
			Uri uri = null;
			string shellUri = "http://schemas.microsoft.com/powershell/Microsoft.Exchange";
			switch (this.connectionType)
			{
			case PowershellConnectionType.WSMan:
				uri = new Uri(string.Format("{0}{1}/wsman", this.useSSL ? "https://" : "http://", this.targetServer));
				shellUri = "http://schemas.microsoft.com/powershell/Microsoft.PowerShell";
				break;
			case PowershellConnectionType.OnPrem:
				this.useSSL = false;
				uri = new Uri(string.Format("{0}{1}/powershell?serializationLevel=Full", this.useSSL ? "https://" : "http://", this.targetServer));
				break;
			case PowershellConnectionType.Tenant:
			{
				string uriString = string.Format("{0}{1}/powershell-liveid?serializationLevel=Full", this.useSSL ? "https://" : "http://", this.targetServer);
				uri = new Uri(uriString);
				break;
			}
			}
			WSManConnectionInfo wsmanConnectionInfo = new WSManConnectionInfo(uri, shellUri, credentials);
			if (this.connectionType == PowershellConnectionType.Tenant)
			{
				wsmanConnectionInfo.AuthenticationMechanism = AuthenticationMechanism.Basic;
			}
			else if (this.connectionType == PowershellConnectionType.OnPrem)
			{
				wsmanConnectionInfo.AuthenticationMechanism = AuthenticationMechanism.Kerberos;
			}
			PowershellHostUI hostUI = new PowershellHostUI();
			RemotePowershellHost host = new RemotePowershellHost(hostUI);
			wsmanConnectionInfo.Culture = sessionUiCulture;
			wsmanConnectionInfo.MaximumConnectionRedirectionCount = 5;
			this.runspace = RunspaceFactory.CreateRunspace(host, wsmanConnectionInfo);
			this.logger.Log(Strings.HybridInfoOpeningRunspace(uri.ToString()));
			this.runspace.Open();
			this.openedRunspace = true;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000051C3 File Offset: 0x000033C3
		public void RunOneCommand(string command, Dictionary<string, object> parameters, bool ignoreNotFoundErrors)
		{
			this.RunOneCommand<object>(command, parameters, ignoreNotFoundErrors);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000051D0 File Offset: 0x000033D0
		public IEnumerable<T> RunOneCommand<T>(string command, Dictionary<string, object> parameters, bool ignoreNotFoundErrors)
		{
			Collection<PSObject> collection = this.RunCommand(command, parameters, ignoreNotFoundErrors);
			List<T> list = new List<T>();
			if (collection.Count > 0)
			{
				if (collection[0] != null && collection[0].BaseObject != null && collection[0].BaseObject is PSCustomObject && MonadCommand.CanDeserialize(collection[0]))
				{
					using (IEnumerator<PSObject> enumerator = collection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PSObject psobject = enumerator.Current;
							if (psobject != null)
							{
								list.Add((T)((object)MonadCommand.Deserialize(psobject)));
							}
						}
						return list;
					}
				}
				throw new InvalidOperationException(Strings.HybridInfoPurePSObjectsNotSupported);
			}
			return list;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000528C File Offset: 0x0000348C
		public T RunOneCommandSingleResult<T>(string command, Dictionary<string, object> parameters, bool ignoreNotFoundErrors)
		{
			IEnumerable<T> source = this.RunOneCommand<T>(command, parameters, ignoreNotFoundErrors);
			if (source.Count<T>() == 0)
			{
				return default(T);
			}
			if (source.Count<T>() == 1)
			{
				return source.First<T>();
			}
			throw new Exception(Strings.TooManyResults);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000052D4 File Offset: 0x000034D4
		public object GetPowershellObjectValueOrNull(string command, string identity, string setting)
		{
			this.logger.Log(Strings.HybridInfoGetObjectValue(setting, identity, command));
			object result = null;
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			Dictionary<string, object> powershellUntypedObjectAsMembers = this.GetPowershellUntypedObjectAsMembers(command, identity, parameters);
			if (!powershellUntypedObjectAsMembers.TryGetValue(setting, out result))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005314 File Offset: 0x00003514
		public Dictionary<string, object> GetPowershellUntypedObjectAsMembers(string command, string identity, Dictionary<string, object> parameters)
		{
			if (!string.IsNullOrEmpty(identity))
			{
				parameters.Add("Identity", identity);
			}
			List<Dictionary<string, object>> powershellUntypedObjectsAsMembers = this.GetPowershellUntypedObjectsAsMembers(command, identity, parameters);
			if (powershellUntypedObjectsAsMembers.Count > 1)
			{
				List<string> list = new List<string>(powershellUntypedObjectsAsMembers.Count);
				foreach (Dictionary<string, object> dictionary in powershellUntypedObjectsAsMembers)
				{
					string item = (dictionary["Identity"] != null) ? dictionary["Identity"].ToString() : string.Empty;
					list.Add(item);
				}
				throw new HybridConfigurationDetectionException(Strings.ErrorTooManyMatchingResults(identity ?? string.Empty));
			}
			return powershellUntypedObjectsAsMembers[0];
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000053D8 File Offset: 0x000035D8
		public List<Dictionary<string, object>> GetPowershellUntypedObjectsAsMembers(string command, string identity, Dictionary<string, object> parameters)
		{
			if (!string.IsNullOrEmpty(identity))
			{
				parameters.Add("Identity", identity);
			}
			Collection<PSObject> collection = this.RunCommand(command, parameters, true);
			if (collection == null || collection.Count == 0)
			{
				this.logger.Log(Strings.HybridInfoObjectNotFound);
				return null;
			}
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			foreach (PSObject psobject in collection)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (PSMemberInfo psmemberInfo in psobject.Members)
				{
					dictionary.Add(psmemberInfo.Name, psmemberInfo.Value);
				}
				list.Add(dictionary);
			}
			return list;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000054BC File Offset: 0x000036BC
		public void SetPowershellObjectProperty(string command, string identity, string property, object value)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(2);
			if (!string.IsNullOrEmpty(identity))
			{
				dictionary.Add("Identity", identity);
			}
			dictionary.Add(property, value);
			this.RunCommand(command, dictionary, false);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000054F7 File Offset: 0x000036F7
		public void CreatePowershellObject(string command, Dictionary<string, object> parameters)
		{
			this.RunCommand(command, parameters, false);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005504 File Offset: 0x00003704
		private Collection<PSObject> RunCommand(string cmdlet, Dictionary<string, object> parameters, bool ignoreNotFoundErrors)
		{
			this.CheckDisposed();
			Collection<PSObject> result;
			using (PowerShell powerShell = PowerShell.Create())
			{
				powerShell.Runspace = this.runspace;
				StringBuilder stringBuilder = new StringBuilder(1024);
				powerShell.AddCommand(cmdlet);
				if (parameters != null && parameters.Count<KeyValuePair<string, object>>() > 0)
				{
					powerShell.AddParameters(parameters);
					foreach (KeyValuePair<string, object> keyValuePair in parameters)
					{
						stringBuilder.Append(string.Format(" -{0} '{1}'", keyValuePair.Key, keyValuePair.Value));
					}
				}
				ExDateTime now = ExDateTime.Now;
				try
				{
					this.logger.Log(Strings.HybridInfoCmdletStart(this.connectionType.ToString(), cmdlet, stringBuilder.ToString()));
					Collection<PSObject> collection = powerShell.Invoke();
					if (powerShell.Streams.Error.Count > 0)
					{
						foreach (ErrorRecord errorRecord in powerShell.Streams.Error)
						{
							if (!errorRecord.CategoryInfo.Reason.Equals("ManagementObjectNotFoundException") || !ignoreNotFoundErrors)
							{
								this.logger.Log(Strings.ErrorWhileRunning(errorRecord.Exception.ToString()));
								throw errorRecord.Exception;
							}
						}
					}
					result = collection;
				}
				catch (Exception innerException)
				{
					Exception ex = new Exception(cmdlet, innerException);
					throw ex;
				}
				finally
				{
					TimeSpan t = ExDateTime.Now.Subtract(now);
					this.totalProcessedTime += t;
					this.logger.Log(Strings.HybridInfoCmdletEnd(this.connectionType.ToString(), cmdlet, t.TotalMilliseconds.ToString()));
				}
			}
			return result;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005748 File Offset: 0x00003948
		private void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("RemotePowershellProxy");
			}
		}

		// Token: 0x040000C2 RID: 194
		private const string ExchangeShellUri = "http://schemas.microsoft.com/powershell/Microsoft.Exchange";

		// Token: 0x040000C3 RID: 195
		private const string WSManShellUri = "http://schemas.microsoft.com/powershell/Microsoft.PowerShell";

		// Token: 0x040000C4 RID: 196
		private const string OnPremUriFormat = "{0}{1}/powershell?serializationLevel=Full";

		// Token: 0x040000C5 RID: 197
		private const string TenantUriFormat = "{0}{1}/powershell-liveid?serializationLevel=Full";

		// Token: 0x040000C6 RID: 198
		private const string WSManUriFormat = "{0}{1}/wsman";

		// Token: 0x040000C7 RID: 199
		private const string Identity = "Identity";

		// Token: 0x040000C8 RID: 200
		private const int MaximumConnectionRedirectionCount = 5;

		// Token: 0x040000C9 RID: 201
		private readonly string targetServer;

		// Token: 0x040000CA RID: 202
		private readonly PowershellConnectionType connectionType;

		// Token: 0x040000CB RID: 203
		private bool useSSL;

		// Token: 0x040000CC RID: 204
		private Runspace runspace;

		// Token: 0x040000CD RID: 205
		private bool openedRunspace;

		// Token: 0x040000CE RID: 206
		private bool isDisposed;

		// Token: 0x040000CF RID: 207
		private TimeSpan totalProcessedTime;

		// Token: 0x040000D0 RID: 208
		private ILogger logger;
	}
}
