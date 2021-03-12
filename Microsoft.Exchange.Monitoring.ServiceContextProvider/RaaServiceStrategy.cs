using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Forefront.RecoveryActionArbiter.Contract;

namespace Microsoft.Exchange.Monitoring.ServiceContextProvider
{
	// Token: 0x02000003 RID: 3
	public class RaaServiceStrategy : IRaaService
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000023B0 File Offset: 0x000005B0
		internal RaaServiceStrategy()
		{
			string forefrontArbitrationServiceUrl = DatacenterRegistry.GetForefrontArbitrationServiceUrl();
			if (forefrontArbitrationServiceUrl != string.Empty)
			{
				this.ServiceBinding = new NetTcpBinding();
				EndpointAddress serviceAddress = new EndpointAddress(forefrontArbitrationServiceUrl);
				this.ServiceAddress = serviceAddress;
				return;
			}
			throw new ArgumentException("The ForefrontArbitrationServiceUrl was not set.");
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023FA File Offset: 0x000005FA
		internal RaaServiceStrategy(Binding binding, EndpointAddress endpointAddress)
		{
			this.ServiceBinding = binding;
			this.ServiceAddress = endpointAddress;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002410 File Offset: 0x00000610
		private RaaServiceProxy ServiceProxy
		{
			get
			{
				if (this.raaServiceProxy == null)
				{
					this.CreateProxy();
				}
				return this.raaServiceProxy;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002426 File Offset: 0x00000626
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000242E File Offset: 0x0000062E
		private EndpointAddress ServiceAddress { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002437 File Offset: 0x00000637
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000243F File Offset: 0x0000063F
		private Binding ServiceBinding { get; set; }

		// Token: 0x06000016 RID: 22 RVA: 0x00002448 File Offset: 0x00000648
		public ApprovalResponse RequestApprovalForRecovery(ApprovalRequest request)
		{
			return this.RequestApprovalForRecovery(request, true);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002452 File Offset: 0x00000652
		public void NotifyRecoveryCompletion(string machineName, bool successfulRecovery)
		{
			this.NotifyRecoveryCompletion(machineName, successfulRecovery, true);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000245D File Offset: 0x0000065D
		public ICollection<AvailabilityData> GetRoleAvailabilityData(string serviceInstance, string role)
		{
			return this.GetRoleAvailabilityData(serviceInstance, role, true);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002468 File Offset: 0x00000668
		private void CreateProxy()
		{
			if (this.raaServiceProxy != null)
			{
				try
				{
					this.raaServiceProxy.Close();
				}
				catch (CommunicationException)
				{
					this.raaServiceProxy.Abort();
				}
				catch (TimeoutException)
				{
					this.raaServiceProxy.Abort();
				}
				catch (Exception)
				{
					this.raaServiceProxy.Abort();
					throw;
				}
			}
			if (this.ServiceAddress == null)
			{
				throw new ArgumentException("The RAA service address was not set.");
			}
			if (this.ServiceBinding == null)
			{
				throw new ArgumentException("The RAA service binding was not set.");
			}
			this.raaServiceProxy = new RaaServiceProxy(this.ServiceBinding, this.ServiceAddress);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002520 File Offset: 0x00000720
		private ICollection<AvailabilityData> GetRoleAvailabilityData(string serviceInstance, string role, bool firstTry)
		{
			ICollection<AvailabilityData> roleAvailabilityData;
			try
			{
				roleAvailabilityData = this.ServiceProxy.GetRoleAvailabilityData(serviceInstance, role);
			}
			catch (Exception ex)
			{
				if (!(ex is CommunicationException) && !(ex is TimeoutException))
				{
					throw;
				}
				this.CreateProxy();
				if (!firstTry)
				{
					throw;
				}
				roleAvailabilityData = this.GetRoleAvailabilityData(serviceInstance, role, false);
			}
			return roleAvailabilityData;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002578 File Offset: 0x00000778
		private ApprovalResponse RequestApprovalForRecovery(ApprovalRequest request, bool firstTry)
		{
			ApprovalResponse result;
			try
			{
				result = this.ServiceProxy.RequestApprovalForRecovery(request);
			}
			catch (Exception ex)
			{
				if (!(ex is CommunicationException) && !(ex is TimeoutException))
				{
					throw;
				}
				this.CreateProxy();
				if (!firstTry)
				{
					throw;
				}
				result = this.RequestApprovalForRecovery(request, false);
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025D0 File Offset: 0x000007D0
		private void NotifyRecoveryCompletion(string machineName, bool successfulRecovery, bool firstTry)
		{
			try
			{
				this.ServiceProxy.NotifyRecoveryCompletion(machineName, true);
			}
			catch (Exception ex)
			{
				if (!(ex is CommunicationException) && !(ex is TimeoutException))
				{
					throw;
				}
				this.CreateProxy();
				if (!firstTry)
				{
					throw;
				}
				this.NotifyRecoveryCompletion(machineName, successfulRecovery, false);
			}
		}

		// Token: 0x04000005 RID: 5
		private RaaServiceProxy raaServiceProxy;
	}
}
