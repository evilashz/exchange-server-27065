using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000098 RID: 152
	public class ProxyWrapper<TProxy, TInterface> : IDisposable where TProxy : ClientBase<TInterface> where TInterface : class
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00005710 File Offset: 0x00003910
		public ProxyWrapper(Uri serviceUri, X509Certificate2 cert)
		{
			BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
			basicHttpBinding.Security.Mode = BasicHttpSecurityMode.Transport;
			basicHttpBinding.MaxReceivedMessageSize = 2147483647L;
			basicHttpBinding.MaxBufferSize = int.MaxValue;
			basicHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
			EndpointAddress endpointAddress = new EndpointAddress(serviceUri, new AddressHeader[0]);
			this.Proxy = (Activator.CreateInstance(typeof(TProxy), new object[]
			{
				basicHttpBinding,
				endpointAddress
			}) as TProxy);
			TProxy proxy = this.Proxy;
			proxy.ClientCredentials.ClientCertificate.Certificate = cert;
			this.disposed = false;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000057BF File Offset: 0x000039BF
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000057C7 File Offset: 0x000039C7
		public TProxy Proxy { get; private set; }

		// Token: 0x060003BE RID: 958 RVA: 0x000057D8 File Offset: 0x000039D8
		public void CallSymphony(Action serviceCall, string epAddress)
		{
			try
			{
				ProxyWrapper<TProxy, TInterface>.CallWCFService<FaultException>(serviceCall, epAddress, delegate(FaultException e)
				{
					ProxyWrapper<TProxy, TInterface>.SymphonyFaultHandler(e);
				}, null);
			}
			catch (MailboxReplicationTransientException innerException)
			{
				throw new MigrationTransientException(ServerStrings.MigrationTransientError(epAddress), innerException);
			}
			catch (MailboxReplicationPermanentException ex)
			{
				throw new MigrationPermanentException(ServerStrings.MigrationPermanentError(epAddress), ex);
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00005844 File Offset: 0x00003A44
		public void Dispose()
		{
			if (this.Proxy != null && !this.disposed)
			{
				try
				{
					TProxy proxy = this.Proxy;
					proxy.Close();
				}
				catch (CommunicationException)
				{
					TProxy proxy2 = this.Proxy;
					proxy2.Abort();
				}
				catch (Exception)
				{
					TProxy proxy3 = this.Proxy;
					proxy3.Abort();
					throw;
				}
				this.disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000058D4 File Offset: 0x00003AD4
		private static void SymphonyFaultHandler(FaultException exception)
		{
			if (exception is FaultException<AccessDeniedFault>)
			{
				throw new SymphonyAccessDeniedFaultException(exception.Message, exception);
			}
			if (exception is FaultException<ArgumentFault>)
			{
				throw new SymphonyArgumentFaultException(exception.Message, exception);
			}
			if (exception is FaultException<CancelNotAllowedFault>)
			{
				throw new SymphonyCancelNotAllowedFaultException(exception.Message, exception);
			}
			if (exception is FaultException<InvalidOperationFault>)
			{
				throw new SymphonyInvalidOperationFaultException(exception.Message, exception);
			}
			throw new SymphonyFaultException(exception.Message, exception);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00005944 File Offset: 0x00003B44
		private static void CallWCFService<ExceptionT>(Action serviceCall, string epAddress, Action<ExceptionT> faultHandler, VersionInformation serverVersion) where ExceptionT : FaultException
		{
			string serviceURI = epAddress;
			if (serverVersion != null)
			{
				serviceURI = string.Format("{0} {1} ({2})", epAddress, serverVersion.ComputerName, serverVersion.ToString());
			}
			try
			{
				serviceCall();
			}
			catch (TimeoutException ex)
			{
				throw new TimeoutErrorTransientException(serviceURI, CommonUtils.FullExceptionMessage(ex), ex);
			}
			catch (ExceptionT exceptionT)
			{
				ExceptionT obj = (ExceptionT)((object)exceptionT);
				faultHandler(obj);
			}
			catch (EndpointNotFoundException ex2)
			{
				throw new EndpointNotFoundTransientException(serviceURI, CommonUtils.FullExceptionMessage(ex2), ex2);
			}
			catch (CommunicationException ex3)
			{
				if (ex3 is FaultException)
				{
					FaultException ex4 = (FaultException)ex3;
					if (ex4.Code != null && ex4.Code.SubCode != null && ex4.Code.IsSenderFault && ex4.Code.SubCode.Name == "DeserializationFailed")
					{
						throw new CommunicationErrorPermanentException(serviceURI, CommonUtils.FullExceptionMessage(ex3), ex3);
					}
				}
				throw new CommunicationErrorTransientException(serviceURI, CommonUtils.FullExceptionMessage(ex3), ex3);
			}
			catch (InvalidOperationException ex5)
			{
				throw new InvalidOperationTransientException(serviceURI, CommonUtils.FullExceptionMessage(ex5), ex5);
			}
			catch (InvalidDataException ex6)
			{
				throw new InvalidDataTransientException(serviceURI, CommonUtils.FullExceptionMessage(ex6), ex6);
			}
		}

		// Token: 0x040001AE RID: 430
		private const int MaxSize = 2147483647;

		// Token: 0x040001AF RID: 431
		private bool disposed;
	}
}
