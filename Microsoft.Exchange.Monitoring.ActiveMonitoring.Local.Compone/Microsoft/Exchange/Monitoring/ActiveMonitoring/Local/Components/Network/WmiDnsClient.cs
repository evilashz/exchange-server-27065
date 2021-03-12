using System;
using System.Linq;
using System.Management;
using System.Net;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network
{
	// Token: 0x02000226 RID: 550
	internal class WmiDnsClient
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x00065C74 File Offset: 0x00063E74
		public WmiDnsClient(string serverFqdn)
		{
			WmiDnsClient.ValidateArgumentNotNullOrWhiteSpace(serverFqdn, "serverFqdn");
			if (!serverFqdn.Contains("."))
			{
				throw new ArgumentException("The server name must be a fully-qualified domain name.", "serverFqdn");
			}
			this.serverFqdn = serverFqdn;
			try
			{
				this.scope = new ManagementScope(new ManagementPath
				{
					Server = serverFqdn,
					NamespacePath = "root\\MicrosoftDNS"
				});
				this.scope.Connect();
			}
			catch (SystemException innerException)
			{
				throw new ApplicationException(string.Format("Failed to connect to DNS service on machine '{0}'.", serverFqdn), innerException);
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00065D0C File Offset: 0x00063F0C
		public IPAddress[] GetForwarders()
		{
			IPAddress[] result;
			using (ManagementObject server = this.GetServer())
			{
				result = WmiDnsClient.ConvertToIpAddresses((string[])server["Forwarders"]);
			}
			return result;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00065D54 File Offset: 0x00063F54
		public void SetForwarders(IPAddress[] forwarders)
		{
			using (ManagementObject server = this.GetServer())
			{
				server["Forwarders"] = WmiDnsClient.ConvertFromIpAddresses(forwarders);
				server.Put();
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00065DA4 File Offset: 0x00063FA4
		private static string[] ConvertFromIpAddresses(IPAddress[] ipAddresses)
		{
			if (ipAddresses == null)
			{
				return null;
			}
			return (from ip in ipAddresses
			select ip.ToString()).ToArray<string>();
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00065DDB File Offset: 0x00063FDB
		private static IPAddress[] ConvertToIpAddresses(string[] stringAddresses)
		{
			if (stringAddresses == null || stringAddresses.Length == 0)
			{
				return null;
			}
			return (from s in stringAddresses
			select IPAddress.Parse(s)).ToArray<IPAddress>();
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00065E0F File Offset: 0x0006400F
		private static void ValidateArgumentNotNull(object argument, string name)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00065E1B File Offset: 0x0006401B
		private static void ValidateArgumentNotNullOrEmpty(string argument, string name)
		{
			if (string.IsNullOrEmpty(argument))
			{
				throw new ArgumentNullException(name, "The value is null or empty.");
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00065E31 File Offset: 0x00064031
		private static void ValidateArgumentNotNullOrWhiteSpace(string argument, string name)
		{
			if (string.IsNullOrWhiteSpace(argument))
			{
				throw new ArgumentNullException(name, "The value is null or white-space.");
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00065E48 File Offset: 0x00064048
		private ManagementObject GetObject(ManagementPath path)
		{
			ManagementObject result;
			try
			{
				ManagementObject managementObject = new ManagementObject
				{
					Scope = this.scope,
					Path = path
				};
				managementObject.Get();
				result = managementObject;
			}
			catch (ManagementException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00065E90 File Offset: 0x00064090
		private ManagementObject GetServer()
		{
			WmiDnsClient.ManagementPathComposable managementPathComposable = new WmiDnsClient.ManagementPathComposable();
			managementPathComposable.ClassName = "MicrosoftDNS_Server";
			managementPathComposable.AppendKey("Name", this.serverFqdn);
			ManagementObject @object = this.GetObject(managementPathComposable);
			if (@object == null)
			{
				throw new ManagementException(string.Format("Unable to acquire the DNS Server instance '{0}'.", managementPathComposable));
			}
			return @object;
		}

		// Token: 0x04000BA0 RID: 2976
		private readonly ManagementScope scope;

		// Token: 0x04000BA1 RID: 2977
		private readonly string serverFqdn;

		// Token: 0x02000227 RID: 551
		private class ManagementPathComposable : ManagementPath
		{
			// Token: 0x06000F62 RID: 3938 RVA: 0x00065EDC File Offset: 0x000640DC
			public ManagementPathComposable()
			{
			}

			// Token: 0x06000F63 RID: 3939 RVA: 0x00065EE4 File Offset: 0x000640E4
			public ManagementPathComposable(string path) : base(path)
			{
			}

			// Token: 0x06000F64 RID: 3940 RVA: 0x00065EED File Offset: 0x000640ED
			public void AppendKey(string key, string value)
			{
				WmiDnsClient.ValidateArgumentNotNull(value, "value");
				value = "\"" + value.Replace("\"", "\\\"") + "\"";
				this.AppendKeyCore(key, value);
			}

			// Token: 0x06000F65 RID: 3941 RVA: 0x00065F23 File Offset: 0x00064123
			public void AppendKey(string key, uint value)
			{
				this.AppendKeyCore(key, value.ToString());
			}

			// Token: 0x06000F66 RID: 3942 RVA: 0x00065F34 File Offset: 0x00064134
			private void AppendKeyCore(string key, string value)
			{
				WmiDnsClient.ValidateArgumentNotNullOrEmpty(key, "key");
				if (!base.IsInstance && !base.IsClass)
				{
					throw new InvalidOperationException("The ClassName property must be set before calling this method.");
				}
				string text = base.RelativePath;
				if (base.IsSingleton)
				{
					text = text.Remove(text.Length - 2);
				}
				base.RelativePath = string.Concat(new string[]
				{
					text,
					(text == base.ClassName) ? "." : ",",
					key,
					"=",
					value
				});
			}
		}
	}
}
