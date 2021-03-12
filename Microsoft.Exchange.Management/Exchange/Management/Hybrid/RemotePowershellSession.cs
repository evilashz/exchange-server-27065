using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Remoting;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000927 RID: 2343
	internal class RemotePowershellSession : IDisposable
	{
		// Token: 0x06005377 RID: 21367 RVA: 0x00158901 File Offset: 0x00156B01
		public RemotePowershellSession(ILogger logger, string targetServer, PowershellConnectionType connectionType, bool useSSL, Func<string, bool> shouldInvokePowershellCommand)
		{
			this.logger = logger;
			this.targetServer = targetServer;
			this.connectionType = connectionType;
			this.useSSL = useSSL;
			this.shouldInvokePowershellCommand = shouldInvokePowershellCommand;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x00158930 File Offset: 0x00156B30
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
				this.logger.LogInformation(HybridStrings.HybridInfoTotalCmdletTime(this.connectionType.ToString(), this.totalProcessedTime.TotalSeconds));
			}
			this.isDisposed = true;
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x001589A4 File Offset: 0x00156BA4
		public void Connect(PSCredential credentials, CultureInfo sessionUiCulture)
		{
			this.CheckDisposed();
			try
			{
				Dns.GetHostEntry(this.targetServer);
			}
			catch (SocketException ex)
			{
				throw new CouldNotResolveServerException(this.targetServer, ex, ex);
			}
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
			this.logger.LogInformation(HybridStrings.HybridInfoOpeningRunspace(uri.ToString()));
			try
			{
				this.runspace.Open();
			}
			catch (PSRemotingTransportException ex2)
			{
				throw new CouldNotOpenRunspaceException(ex2, ex2);
			}
			this.openedRunspace = true;
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x00158B44 File Offset: 0x00156D44
		public void RunOneCommand(string command, SessionParameters parameters, bool ignoreNotFoundErrors)
		{
			this.RunOneCommand<object>(command, parameters, ignoreNotFoundErrors);
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x00158B50 File Offset: 0x00156D50
		public IEnumerable<T> RunOneCommand<T>(string command, SessionParameters parameters, bool ignoreNotFoundErrors)
		{
			Collection<PSObject> collection = this.RunCommand(command, parameters, ignoreNotFoundErrors);
			List<T> list = new List<T>();
			if (collection != null && collection.Count > 0)
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
				throw new InvalidOperationException(HybridStrings.HybridInfoPurePSObjectsNotSupported);
			}
			return list;
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x00158C10 File Offset: 0x00156E10
		public T RunOneCommandSingleResult<T>(string command, SessionParameters parameters, bool ignoreNotFoundErrors)
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
			throw new Exception("To many results returned.  Only one result expected.");
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x00158C54 File Offset: 0x00156E54
		public object GetPowershellObjectValueOrNull(string command, string identity, string setting)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			stringBuilder.Append(HybridStrings.HybridInfoGetObjectValue(setting, identity, command));
			this.logger.LogInformation(stringBuilder.ToString());
			object result = null;
			SessionParameters parameters = new SessionParameters();
			Dictionary<string, object> powershellUntypedObjectAsMembers = this.GetPowershellUntypedObjectAsMembers(command, identity, parameters);
			if (!powershellUntypedObjectAsMembers.TryGetValue(setting, out result))
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x00158CB4 File Offset: 0x00156EB4
		public Dictionary<string, object> GetPowershellUntypedObjectAsMembers(string command, string identity, SessionParameters parameters)
		{
			if (!string.IsNullOrEmpty(identity))
			{
				parameters.Set("Identity", identity);
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
				throw new TooManyResultsException(identity ?? string.Empty, HybridStrings.ErrorTooManyMatchingResults(identity ?? string.Empty), null, list);
			}
			return powershellUntypedObjectsAsMembers[0];
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x00158D88 File Offset: 0x00156F88
		public List<Dictionary<string, object>> GetPowershellUntypedObjectsAsMembers(string command, string identity, SessionParameters parameters)
		{
			if (!string.IsNullOrEmpty(identity))
			{
				parameters.Set("Identity", identity);
			}
			Collection<PSObject> collection = this.RunCommand(command, parameters, true);
			if (collection == null || collection.Count == 0)
			{
				this.logger.LogInformation(HybridStrings.HybridInfoObjectNotFound);
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

		// Token: 0x06005380 RID: 21376 RVA: 0x00158E74 File Offset: 0x00157074
		public Collection<PSObject> RunCommand(string cmdlet, SessionParameters parameters)
		{
			return this.RunCommand(cmdlet, parameters, true);
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x00158E80 File Offset: 0x00157080
		public Collection<PSObject> RunCommand(string cmdlet, SessionParameters parameters, bool ignoreNotFoundErrors)
		{
			string data = null;
			string text = this.BuildCmdletInvocationForLogging(cmdlet, parameters);
			if (this.shouldInvokePowershellCommand(text))
			{
				this.CheckDisposed();
				using (PowerShell powerShell = PowerShell.Create())
				{
					powerShell.Runspace = this.runspace;
					powerShell.AddCommand(cmdlet);
					if (parameters != null && parameters.Count > 0)
					{
						powerShell.AddParameters(parameters.ToDictionary());
					}
					ExDateTime now = ExDateTime.Now;
					try
					{
						this.logger.LogInformation(HybridStrings.HybridInfoCmdletStart(this.connectionType.ToString(), text, string.Empty));
						Collection<PSObject> collection = powerShell.Invoke();
						if (powerShell.Streams.Error.Count > 0)
						{
							foreach (ErrorRecord errorRecord in powerShell.Streams.Error)
							{
								if (!errorRecord.CategoryInfo.Reason.Equals("ManagementObjectNotFoundException") || !ignoreNotFoundErrors)
								{
									this.logger.LogError(errorRecord.Exception.ToString());
									throw errorRecord.Exception;
								}
							}
						}
						try
						{
							data = RemotePowershellSession.ToText(collection);
						}
						catch
						{
							data = "?";
						}
						return collection;
					}
					catch (Exception innerException)
					{
						Exception ex = new Exception(HybridStrings.ErrorCmdletException(cmdlet).ToString(), innerException);
						throw ex;
					}
					finally
					{
						TimeSpan t = ExDateTime.Now.Subtract(now);
						this.totalProcessedTime += t;
						this.LogInformationAndData(HybridStrings.HybridInfoCmdletEnd(this.connectionType.ToString(), cmdlet, t.TotalMilliseconds.ToString()), data);
					}
				}
			}
			return null;
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x001590C4 File Offset: 0x001572C4
		private static string ToText(IEnumerable<PSObject> objects)
		{
			return "[" + string.Join(", ", from o in objects
			select RemotePowershellSession.ToText(o)) + "]";
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x00159124 File Offset: 0x00157324
		private static string ToText(PSObject o)
		{
			if (o.ImmediateBaseObject is ArrayList)
			{
				return "[" + string.Join(", ", ((ArrayList)o.ImmediateBaseObject).ToArray().Select(delegate(object i)
				{
					if (i != null)
					{
						return i.ToString();
					}
					return "(null)";
				})) + "]";
			}
			return "{ " + string.Join(", ", from p in o.Properties
			orderby p.Name
			select RemotePowershellSession.ToText(p)) + "}";
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x001591ED File Offset: 0x001573ED
		private static string ToText(PSPropertyInfo p)
		{
			return string.Format("\"{0}\": {1}", p.Name, RemotePowershellSession.ToText(p.TypeNameOfValue, p.Value));
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x00159210 File Offset: 0x00157410
		private static string ToText(string type, object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			if (value is PSObject)
			{
				return RemotePowershellSession.ToText((PSObject)value);
			}
			return "\"" + value.ToString() + "\"";
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x00159244 File Offset: 0x00157444
		private string BuildCmdletInvocationForLogging(string cmdlet, SessionParameters parameters)
		{
			StringBuilder stringBuilder = new StringBuilder(cmdlet);
			if (parameters != null && parameters.Count > 0)
			{
				stringBuilder.AppendFormat(" {0}", string.Join(" ", parameters.LoggingText.ToArray<string>()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0015928B File Offset: 0x0015748B
		private void CheckDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("RemotePowershellProxy");
			}
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x001592A0 File Offset: 0x001574A0
		private void LogInformationAndData(string text, string data)
		{
			if (this.logger is Logger)
			{
				((Logger)this.logger).LogInformation(text, data);
				return;
			}
			this.logger.LogInformation(text);
		}

		// Token: 0x040030D5 RID: 12501
		private const string ExchangeShellUri = "http://schemas.microsoft.com/powershell/Microsoft.Exchange";

		// Token: 0x040030D6 RID: 12502
		private const string WSManShellUri = "http://schemas.microsoft.com/powershell/Microsoft.PowerShell";

		// Token: 0x040030D7 RID: 12503
		private const string OnPremUriFormat = "{0}{1}/powershell?serializationLevel=Full";

		// Token: 0x040030D8 RID: 12504
		private const string TenantUriFormat = "{0}{1}/powershell-liveid?serializationLevel=Full";

		// Token: 0x040030D9 RID: 12505
		private const string WSManUriFormat = "{0}{1}/wsman";

		// Token: 0x040030DA RID: 12506
		private const int MaximumConnectionRedirectionCount = 5;

		// Token: 0x040030DB RID: 12507
		private readonly string targetServer;

		// Token: 0x040030DC RID: 12508
		private readonly PowershellConnectionType connectionType;

		// Token: 0x040030DD RID: 12509
		private bool useSSL;

		// Token: 0x040030DE RID: 12510
		private Runspace runspace;

		// Token: 0x040030DF RID: 12511
		private bool openedRunspace;

		// Token: 0x040030E0 RID: 12512
		private bool isDisposed;

		// Token: 0x040030E1 RID: 12513
		private readonly ILogger logger;

		// Token: 0x040030E2 RID: 12514
		private TimeSpan totalProcessedTime;

		// Token: 0x040030E3 RID: 12515
		private Func<string, bool> shouldInvokePowershellCommand;
	}
}
