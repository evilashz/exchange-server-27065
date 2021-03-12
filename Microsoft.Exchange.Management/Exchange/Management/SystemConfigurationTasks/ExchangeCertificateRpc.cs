using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc.ExchangeCertificate;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AC2 RID: 2754
	internal sealed class ExchangeCertificateRpc
	{
		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x06006140 RID: 24896 RVA: 0x00195FC8 File Offset: 0x001941C8
		// (set) Token: 0x06006141 RID: 24897 RVA: 0x00195FED File Offset: 0x001941ED
		public string GetByThumbprint
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.GetByThumbprint, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.GetByThumbprint] = value;
			}
		}

		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x06006142 RID: 24898 RVA: 0x00195FFC File Offset: 0x001941FC
		// (set) Token: 0x06006143 RID: 24899 RVA: 0x00196021 File Offset: 0x00194221
		public byte[] GetByCertificate
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.GetByCertificate, out obj))
				{
					return (byte[])obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.GetByCertificate] = value;
			}
		}

		// Token: 0x17001D7E RID: 7550
		// (get) Token: 0x06006144 RID: 24900 RVA: 0x00196030 File Offset: 0x00194230
		// (set) Token: 0x06006145 RID: 24901 RVA: 0x00196055 File Offset: 0x00194255
		public MultiValuedProperty<SmtpDomain> GetByDomains
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.GetByDomains, out obj))
				{
					return (MultiValuedProperty<SmtpDomain>)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.GetByDomains] = value;
			}
		}

		// Token: 0x17001D7F RID: 7551
		// (get) Token: 0x06006146 RID: 24902 RVA: 0x00196064 File Offset: 0x00194264
		// (set) Token: 0x06006147 RID: 24903 RVA: 0x0019608A File Offset: 0x0019428A
		public bool CreateExportable
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.CreateExportable, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateExportable] = value;
			}
		}

		// Token: 0x17001D80 RID: 7552
		// (get) Token: 0x06006148 RID: 24904 RVA: 0x001960A0 File Offset: 0x001942A0
		// (set) Token: 0x06006149 RID: 24905 RVA: 0x001960C6 File Offset: 0x001942C6
		public string CreateSubjectName
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.CreateSubjectName, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateSubjectName] = value;
			}
		}

		// Token: 0x17001D81 RID: 7553
		// (get) Token: 0x0600614A RID: 24906 RVA: 0x001960D8 File Offset: 0x001942D8
		// (set) Token: 0x0600614B RID: 24907 RVA: 0x001960FE File Offset: 0x001942FE
		public string CreateFriendlyName
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.CreateFriendlyName, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateFriendlyName] = value;
			}
		}

		// Token: 0x17001D82 RID: 7554
		// (get) Token: 0x0600614C RID: 24908 RVA: 0x00196110 File Offset: 0x00194310
		// (set) Token: 0x0600614D RID: 24909 RVA: 0x00196136 File Offset: 0x00194336
		public MultiValuedProperty<SmtpDomainWithSubdomains> CreateDomains
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.CreateDomains, out obj))
				{
					return (MultiValuedProperty<SmtpDomainWithSubdomains>)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateDomains] = value;
			}
		}

		// Token: 0x17001D83 RID: 7555
		// (get) Token: 0x0600614E RID: 24910 RVA: 0x00196148 File Offset: 0x00194348
		// (set) Token: 0x0600614F RID: 24911 RVA: 0x0019616E File Offset: 0x0019436E
		public bool CreateIncAccepted
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.CreateIncAccepted, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateIncAccepted] = value;
			}
		}

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x06006150 RID: 24912 RVA: 0x00196184 File Offset: 0x00194384
		// (set) Token: 0x06006151 RID: 24913 RVA: 0x001961AA File Offset: 0x001943AA
		public bool CreateIncFqdn
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.CreateIncFqdn, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateIncFqdn] = value;
			}
		}

		// Token: 0x17001D85 RID: 7557
		// (get) Token: 0x06006152 RID: 24914 RVA: 0x001961C0 File Offset: 0x001943C0
		// (set) Token: 0x06006153 RID: 24915 RVA: 0x001961E6 File Offset: 0x001943E6
		public bool CreateIncNetBios
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.CreateIncNetBios, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateIncNetBios] = value;
			}
		}

		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x06006154 RID: 24916 RVA: 0x001961FC File Offset: 0x001943FC
		// (set) Token: 0x06006155 RID: 24917 RVA: 0x00196222 File Offset: 0x00194422
		public bool CreateIncAutoDisc
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.CreateIncAutoDisc, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateIncAutoDisc] = value;
			}
		}

		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x06006156 RID: 24918 RVA: 0x00196238 File Offset: 0x00194438
		// (set) Token: 0x06006157 RID: 24919 RVA: 0x0019625E File Offset: 0x0019445E
		public int CreateKeySize
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.CreateKeySize, out obj))
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateKeySize] = value;
			}
		}

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x06006158 RID: 24920 RVA: 0x00196274 File Offset: 0x00194474
		// (set) Token: 0x06006159 RID: 24921 RVA: 0x0019629A File Offset: 0x0019449A
		public byte[] CreateCloneCert
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.CreateCloneCert, out obj))
				{
					return (byte[])obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateCloneCert] = value;
			}
		}

		// Token: 0x17001D89 RID: 7561
		// (get) Token: 0x0600615A RID: 24922 RVA: 0x001962AC File Offset: 0x001944AC
		// (set) Token: 0x0600615B RID: 24923 RVA: 0x001962D2 File Offset: 0x001944D2
		public bool CreateBinary
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.CreateBinary, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateBinary] = value;
			}
		}

		// Token: 0x17001D8A RID: 7562
		// (get) Token: 0x0600615C RID: 24924 RVA: 0x001962E8 File Offset: 0x001944E8
		// (set) Token: 0x0600615D RID: 24925 RVA: 0x0019630E File Offset: 0x0019450E
		public bool CreateRequest
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.CreateRequest, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateRequest] = value;
			}
		}

		// Token: 0x17001D8B RID: 7563
		// (get) Token: 0x0600615E RID: 24926 RVA: 0x00196324 File Offset: 0x00194524
		// (set) Token: 0x0600615F RID: 24927 RVA: 0x0019634A File Offset: 0x0019454A
		public AllowedServices CreateServices
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.CreateServices, out obj))
				{
					return (AllowedServices)obj;
				}
				return AllowedServices.None;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateServices] = value;
			}
		}

		// Token: 0x17001D8C RID: 7564
		// (get) Token: 0x06006160 RID: 24928 RVA: 0x00196360 File Offset: 0x00194560
		// (set) Token: 0x06006161 RID: 24929 RVA: 0x00196386 File Offset: 0x00194586
		public bool RequireSsl
		{
			get
			{
				object obj;
				return !this.inputParameters.TryGetValue(RpcParameters.RequireSsl, out obj) || (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.RequireSsl] = value;
			}
		}

		// Token: 0x17001D8D RID: 7565
		// (get) Token: 0x06006162 RID: 24930 RVA: 0x0019639C File Offset: 0x0019459C
		// (set) Token: 0x06006163 RID: 24931 RVA: 0x001963C2 File Offset: 0x001945C2
		public string CreateSubjectKeyIdentifier
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.CreateSubjectKeyIdentifier, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateSubjectKeyIdentifier] = value;
			}
		}

		// Token: 0x17001D8E RID: 7566
		// (get) Token: 0x06006164 RID: 24932 RVA: 0x001963D4 File Offset: 0x001945D4
		// (set) Token: 0x06006165 RID: 24933 RVA: 0x001963FA File Offset: 0x001945FA
		public bool CreateAllowConfirmation
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.CreateAllowConfirmation, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateAllowConfirmation] = value;
			}
		}

		// Token: 0x17001D8F RID: 7567
		// (get) Token: 0x06006166 RID: 24934 RVA: 0x00196410 File Offset: 0x00194610
		// (set) Token: 0x06006167 RID: 24935 RVA: 0x00196436 File Offset: 0x00194636
		public bool CreateWhatIf
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.CreateWhatIf, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.CreateWhatIf] = value;
			}
		}

		// Token: 0x17001D90 RID: 7568
		// (get) Token: 0x06006168 RID: 24936 RVA: 0x0019644C File Offset: 0x0019464C
		// (set) Token: 0x06006169 RID: 24937 RVA: 0x00196475 File Offset: 0x00194675
		public string RemoveByThumbprint
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.RemoveByThumbprint, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.RemoveByThumbprint] = value;
			}
		}

		// Token: 0x17001D91 RID: 7569
		// (get) Token: 0x0600616A RID: 24938 RVA: 0x00196488 File Offset: 0x00194688
		// (set) Token: 0x0600616B RID: 24939 RVA: 0x001964B1 File Offset: 0x001946B1
		public string ExportByThumbprint
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.ExportByThumbprint, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.ExportByThumbprint] = value;
			}
		}

		// Token: 0x17001D92 RID: 7570
		// (get) Token: 0x0600616C RID: 24940 RVA: 0x001964C4 File Offset: 0x001946C4
		// (set) Token: 0x0600616D RID: 24941 RVA: 0x001964ED File Offset: 0x001946ED
		public bool ExportBinary
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.ExportBinary, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.ExportBinary] = value;
			}
		}

		// Token: 0x17001D93 RID: 7571
		// (get) Token: 0x0600616E RID: 24942 RVA: 0x00196508 File Offset: 0x00194708
		// (set) Token: 0x0600616F RID: 24943 RVA: 0x00196531 File Offset: 0x00194731
		public string ImportCert
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.ImportCert, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.ImportCert] = value;
			}
		}

		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x06006170 RID: 24944 RVA: 0x00196544 File Offset: 0x00194744
		// (set) Token: 0x06006171 RID: 24945 RVA: 0x0019656D File Offset: 0x0019476D
		public string ImportDescription
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.ImportDescription, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.ImportDescription] = value;
			}
		}

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x06006172 RID: 24946 RVA: 0x00196580 File Offset: 0x00194780
		// (set) Token: 0x06006173 RID: 24947 RVA: 0x001965A9 File Offset: 0x001947A9
		public bool ImportExportable
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.ImportExportable, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.ImportExportable] = value;
			}
		}

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x06006174 RID: 24948 RVA: 0x001965C4 File Offset: 0x001947C4
		// (set) Token: 0x06006175 RID: 24949 RVA: 0x001965ED File Offset: 0x001947ED
		public string EnableByThumbprint
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.EnableByThumbprint, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.inputParameters[RpcParameters.EnableByThumbprint] = value;
			}
		}

		// Token: 0x17001D97 RID: 7575
		// (get) Token: 0x06006176 RID: 24950 RVA: 0x00196600 File Offset: 0x00194800
		// (set) Token: 0x06006177 RID: 24951 RVA: 0x00196629 File Offset: 0x00194829
		public AllowedServices EnableServices
		{
			get
			{
				object obj;
				if (this.inputParameters.TryGetValue(RpcParameters.EnableServices, out obj))
				{
					return (AllowedServices)obj;
				}
				return AllowedServices.None;
			}
			set
			{
				this.inputParameters[RpcParameters.EnableServices] = value;
			}
		}

		// Token: 0x17001D98 RID: 7576
		// (get) Token: 0x06006178 RID: 24952 RVA: 0x00196644 File Offset: 0x00194844
		// (set) Token: 0x06006179 RID: 24953 RVA: 0x0019666D File Offset: 0x0019486D
		public bool EnableAllowConfirmation
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.EnableAllowConfirmation, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.EnableAllowConfirmation] = value;
			}
		}

		// Token: 0x17001D99 RID: 7577
		// (get) Token: 0x0600617A RID: 24954 RVA: 0x00196688 File Offset: 0x00194888
		// (set) Token: 0x0600617B RID: 24955 RVA: 0x001966B1 File Offset: 0x001948B1
		public bool EnableUpdateAD
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.EnableUpdateAD, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.EnableUpdateAD] = value;
			}
		}

		// Token: 0x17001D9A RID: 7578
		// (get) Token: 0x0600617C RID: 24956 RVA: 0x001966CC File Offset: 0x001948CC
		// (set) Token: 0x0600617D RID: 24957 RVA: 0x001966F5 File Offset: 0x001948F5
		public bool EnableNetworkService
		{
			get
			{
				object obj;
				return this.inputParameters.TryGetValue(RpcParameters.EnableNetworkService, out obj) && (bool)obj;
			}
			set
			{
				this.inputParameters[RpcParameters.EnableNetworkService] = value;
			}
		}

		// Token: 0x17001D9B RID: 7579
		// (get) Token: 0x0600617E RID: 24958 RVA: 0x00196710 File Offset: 0x00194910
		// (set) Token: 0x0600617F RID: 24959 RVA: 0x00196735 File Offset: 0x00194935
		public List<ExchangeCertificate> ReturnCertList
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.ExchangeCertList, out obj))
				{
					return (List<ExchangeCertificate>)obj;
				}
				return null;
			}
			set
			{
				this.outputParameters[RpcOutput.ExchangeCertList] = value;
			}
		}

		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x06006180 RID: 24960 RVA: 0x00196744 File Offset: 0x00194944
		// (set) Token: 0x06006181 RID: 24961 RVA: 0x00196769 File Offset: 0x00194969
		public ExchangeCertificate ReturnCert
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.ExchangeCert, out obj))
				{
					return (ExchangeCertificate)obj;
				}
				return null;
			}
			set
			{
				this.outputParameters[RpcOutput.ExchangeCert] = value;
			}
		}

		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x06006182 RID: 24962 RVA: 0x00196778 File Offset: 0x00194978
		// (set) Token: 0x06006183 RID: 24963 RVA: 0x0019679E File Offset: 0x0019499E
		public string ReturnCertRequest
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.CertRequest, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.outputParameters[RpcOutput.CertRequest] = value;
			}
		}

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x06006184 RID: 24964 RVA: 0x001967B0 File Offset: 0x001949B0
		// (set) Token: 0x06006185 RID: 24965 RVA: 0x001967D9 File Offset: 0x001949D9
		public string ReturnExportBase64
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.ExportBase64, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.outputParameters[RpcOutput.ExportBase64] = value;
			}
		}

		// Token: 0x17001D9F RID: 7583
		// (get) Token: 0x06006186 RID: 24966 RVA: 0x001967EC File Offset: 0x001949EC
		// (set) Token: 0x06006187 RID: 24967 RVA: 0x00196815 File Offset: 0x00194A15
		public byte[] ReturnExportFileData
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.ExportFile, out obj))
				{
					return (byte[])obj;
				}
				return null;
			}
			set
			{
				this.outputParameters[RpcOutput.ExportFile] = value;
			}
		}

		// Token: 0x17001DA0 RID: 7584
		// (get) Token: 0x06006188 RID: 24968 RVA: 0x00196828 File Offset: 0x00194A28
		// (set) Token: 0x06006189 RID: 24969 RVA: 0x00196851 File Offset: 0x00194A51
		public bool ReturnExportBinary
		{
			get
			{
				object obj;
				return this.outputParameters.TryGetValue(RpcOutput.ExportBinary, out obj) && (bool)obj;
			}
			set
			{
				this.outputParameters[RpcOutput.ExportBinary] = value;
			}
		}

		// Token: 0x17001DA1 RID: 7585
		// (get) Token: 0x0600618A RID: 24970 RVA: 0x0019686C File Offset: 0x00194A6C
		// (set) Token: 0x0600618B RID: 24971 RVA: 0x00196895 File Offset: 0x00194A95
		public bool ReturnExportPKCS10
		{
			get
			{
				object obj;
				return this.outputParameters.TryGetValue(RpcOutput.ExportPKCS10, out obj) && (bool)obj;
			}
			set
			{
				this.outputParameters[RpcOutput.ExportPKCS10] = value;
			}
		}

		// Token: 0x17001DA2 RID: 7586
		// (get) Token: 0x0600618C RID: 24972 RVA: 0x001968B0 File Offset: 0x00194AB0
		// (set) Token: 0x0600618D RID: 24973 RVA: 0x001968DD File Offset: 0x00194ADD
		public LocalizedString ReturnConfirmation
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.TaskConfirmation, out obj))
				{
					return (LocalizedString)obj;
				}
				return LocalizedString.Empty;
			}
			set
			{
				this.outputParameters[RpcOutput.TaskConfirmation] = value;
			}
		}

		// Token: 0x17001DA3 RID: 7587
		// (get) Token: 0x0600618E RID: 24974 RVA: 0x001968F8 File Offset: 0x00194AF8
		// (set) Token: 0x0600618F RID: 24975 RVA: 0x00196921 File Offset: 0x00194B21
		public Dictionary<AllowedServices, LocalizedString> ReturnConfirmationList
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.TaskConfirmationList, out obj))
				{
					return (Dictionary<AllowedServices, LocalizedString>)obj;
				}
				return null;
			}
			set
			{
				this.outputParameters[RpcOutput.TaskConfirmationList] = value;
			}
		}

		// Token: 0x17001DA4 RID: 7588
		// (get) Token: 0x06006190 RID: 24976 RVA: 0x00196934 File Offset: 0x00194B34
		// (set) Token: 0x06006191 RID: 24977 RVA: 0x0019695D File Offset: 0x00194B5D
		public string ReturnTaskErrorString
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.TaskErrorString, out obj))
				{
					return (string)obj;
				}
				return null;
			}
			set
			{
				this.outputParameters[RpcOutput.TaskErrorString] = value;
			}
		}

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x06006192 RID: 24978 RVA: 0x00196970 File Offset: 0x00194B70
		// (set) Token: 0x06006193 RID: 24979 RVA: 0x00196999 File Offset: 0x00194B99
		public ErrorCategory ReturnTaskErrorCategory
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.TaskErrorCategory, out obj))
				{
					return (ErrorCategory)obj;
				}
				return ErrorCategory.NotSpecified;
			}
			set
			{
				this.outputParameters[RpcOutput.TaskErrorCategory] = value;
			}
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x06006194 RID: 24980 RVA: 0x001969B4 File Offset: 0x00194BB4
		// (set) Token: 0x06006195 RID: 24981 RVA: 0x001969DD File Offset: 0x00194BDD
		public List<LocalizedString> ReturnTaskWarningList
		{
			get
			{
				object obj;
				if (this.outputParameters.TryGetValue(RpcOutput.TaskWarningList, out obj))
				{
					return (List<LocalizedString>)obj;
				}
				return null;
			}
			set
			{
				this.outputParameters[RpcOutput.TaskWarningList] = value;
			}
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x001969F0 File Offset: 0x00194BF0
		public ExchangeCertificateRpc(ExchangeCertificateRpcVersion version, byte[] inputBlob, byte[] outputBlob)
		{
			this.inputParameters = new Dictionary<RpcParameters, object>();
			if (inputBlob != null)
			{
				if (version == ExchangeCertificateRpcVersion.Version1)
				{
					this.inputParameters = (Dictionary<RpcParameters, object>)this.DeserializeObject(inputBlob, false);
				}
				else if (version == ExchangeCertificateRpcVersion.Version2)
				{
					this.inputParameters = this.BuildInputParameters(inputBlob);
				}
			}
			this.outputParameters = new Dictionary<RpcOutput, object>();
			if (outputBlob != null)
			{
				if (version == ExchangeCertificateRpcVersion.Version1)
				{
					this.outputParameters = (Dictionary<RpcOutput, object>)this.DeserializeObject(outputBlob, false);
					return;
				}
				if (version == ExchangeCertificateRpcVersion.Version2)
				{
					this.outputParameters = this.BuildOutputParameters(outputBlob);
				}
			}
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x00196A70 File Offset: 0x00194C70
		public ExchangeCertificateRpc() : this(ExchangeCertificateRpcVersion.Version1, null, null)
		{
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x00196A7C File Offset: 0x00194C7C
		internal static byte[] SerializeError(ExchangeCertificateRpcVersion version, string message, ErrorCategory category)
		{
			if (version == ExchangeCertificateRpcVersion.Version2)
			{
				return ExchangeCertificateRpc.SerializeObject(new object[]
				{
					ExchangeCertificateRpc.SerializeObject(RpcOutput.TaskErrorString),
					ExchangeCertificateRpc.SerializeObject(message),
					ExchangeCertificateRpc.SerializeObject(RpcOutput.TaskErrorCategory),
					ExchangeCertificateRpc.SerializeObject(category)
				});
			}
			Dictionary<RpcOutput, object> dictionary = new Dictionary<RpcOutput, object>();
			dictionary[RpcOutput.TaskErrorString] = message;
			dictionary[RpcOutput.TaskErrorCategory] = category;
			return ExchangeCertificateRpc.SerializeObject(dictionary);
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x00196B00 File Offset: 0x00194D00
		internal static void OutputTaskMessages(Server server, ExchangeCertificateRpc outputValues, Task.TaskWarningLoggingDelegate warningWriter, Task.TaskErrorLoggingDelegate errorWriter)
		{
			if (outputValues.ReturnTaskWarningList != null)
			{
				foreach (LocalizedString message in outputValues.ReturnTaskWarningList)
				{
					warningWriter(message);
				}
			}
			if (!string.IsNullOrEmpty(outputValues.ReturnTaskErrorString))
			{
				errorWriter(new InvalidOperationException(Strings.DetailRpcError(server.Name, outputValues.ReturnTaskErrorString)), outputValues.ReturnTaskErrorCategory, null);
			}
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x00196B90 File Offset: 0x00194D90
		internal byte[] SerializeInputParameters(ExchangeCertificateRpcVersion rpcVersion)
		{
			if (rpcVersion == ExchangeCertificateRpcVersion.Version2)
			{
				return this.SerializeDictionaryAsArray<RpcParameters, object>(this.inputParameters);
			}
			return ExchangeCertificateRpc.SerializeObject(this.inputParameters);
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x00196BAE File Offset: 0x00194DAE
		internal byte[] SerializeOutputParameters(ExchangeCertificateRpcVersion rpcVersion)
		{
			if (rpcVersion == ExchangeCertificateRpcVersion.Version2)
			{
				return this.SerializeOutputParametersAsArray();
			}
			return ExchangeCertificateRpc.SerializeObject(this.outputParameters);
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x00196BC8 File Offset: 0x00194DC8
		private byte[] SerializeDictionaryAsArray<TKey, TValue>(Dictionary<TKey, TValue> list)
		{
			int num = 0;
			if (list != null)
			{
				object[] array = new object[list.Count * 2];
				foreach (KeyValuePair<TKey, TValue> keyValuePair in list)
				{
					array[num++] = ExchangeCertificateRpc.SerializeObject(keyValuePair.Key);
					array[num++] = ExchangeCertificateRpc.SerializeObject(keyValuePair.Value);
				}
				return ExchangeCertificateRpc.SerializeObject(array);
			}
			return null;
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x00196C5C File Offset: 0x00194E5C
		private byte[] SerializeListAsArray<TItem>(List<TItem> list)
		{
			int num = 0;
			if (list != null)
			{
				object[] array = new object[list.Count];
				foreach (TItem titem in list)
				{
					if (typeof(TItem) == typeof(ExchangeCertificate))
					{
						ExchangeCertificate exchangeCertificate = titem as ExchangeCertificate;
						array[num++] = ExchangeCertificateRpc.SerializeObject(exchangeCertificate.ExchangeCertificateAsArray());
					}
					else
					{
						array[num++] = ExchangeCertificateRpc.SerializeObject(titem);
					}
				}
				return ExchangeCertificateRpc.SerializeObject(array);
			}
			return null;
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x00196D10 File Offset: 0x00194F10
		private byte[] SerializeOutputParametersAsArray()
		{
			int num = 0;
			object[] array = new object[this.outputParameters.Count * 2];
			foreach (KeyValuePair<RpcOutput, object> keyValuePair in this.outputParameters)
			{
				array[num++] = ExchangeCertificateRpc.SerializeObject(keyValuePair.Key);
				RpcOutput key = keyValuePair.Key;
				switch (key)
				{
				case RpcOutput.ExchangeCertList:
					array[num++] = this.SerializeListAsArray<ExchangeCertificate>(keyValuePair.Value as List<ExchangeCertificate>);
					break;
				case RpcOutput.ExchangeCert:
					array[num++] = ExchangeCertificateRpc.SerializeObject(((ExchangeCertificate)keyValuePair.Value).ExchangeCertificateAsArray());
					break;
				default:
					switch (key)
					{
					case RpcOutput.TaskWarningList:
						array[num++] = this.SerializeListAsArray<LocalizedString>(keyValuePair.Value as List<LocalizedString>);
						break;
					case RpcOutput.TaskConfirmationList:
						array[num++] = this.SerializeDictionaryAsArray<AllowedServices, LocalizedString>(keyValuePair.Value as Dictionary<AllowedServices, LocalizedString>);
						break;
					default:
						array[num++] = ExchangeCertificateRpc.SerializeObject(keyValuePair.Value);
						break;
					}
					break;
				}
			}
			return ExchangeCertificateRpc.SerializeObject(array);
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x00196E48 File Offset: 0x00195048
		internal Dictionary<RpcParameters, object> BuildInputParameters(byte[] blob)
		{
			Dictionary<RpcParameters, object> dictionary = new Dictionary<RpcParameters, object>();
			object[] array = (object[])this.DeserializeObject(blob, false);
			if (array.Length % 2 == 0)
			{
				for (int i = 0; i < array.Length; i += 2)
				{
					RpcParameters key = (RpcParameters)this.DeserializeObject((byte[])array[i], true);
					object value = this.DeserializeObject((byte[])array[i + 1], true);
					dictionary[key] = value;
				}
			}
			return dictionary;
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x00196EB0 File Offset: 0x001950B0
		internal Dictionary<RpcOutput, object> BuildOutputParameters(byte[] blob)
		{
			Dictionary<RpcOutput, object> dictionary = new Dictionary<RpcOutput, object>();
			object[] array = (object[])this.DeserializeObject(blob, false);
			for (int i = 0; i < array.Length; i += 2)
			{
				RpcOutput rpcOutput = (RpcOutput)this.DeserializeObject((byte[])array[i], true);
				object obj = this.DeserializeObject((byte[])array[i + 1], true);
				if (obj != null)
				{
					RpcOutput rpcOutput2 = rpcOutput;
					switch (rpcOutput2)
					{
					case RpcOutput.ExchangeCertList:
					{
						List<ExchangeCertificate> list = new List<ExchangeCertificate>();
						foreach (object obj2 in (object[])obj)
						{
							list.Add(new ExchangeCertificate((object[])this.DeserializeObject((byte[])obj2, true)));
						}
						obj = list;
						break;
					}
					case RpcOutput.ExchangeCert:
						obj = new ExchangeCertificate((object[])obj);
						break;
					default:
						switch (rpcOutput2)
						{
						case RpcOutput.TaskWarningList:
						{
							List<LocalizedString> list2 = new List<LocalizedString>();
							foreach (object obj3 in (object[])obj)
							{
								list2.Add((LocalizedString)this.DeserializeObject((byte[])obj3, true));
							}
							obj = list2;
							break;
						}
						case RpcOutput.TaskConfirmationList:
						{
							object[] array4 = (object[])obj;
							Dictionary<AllowedServices, LocalizedString> dictionary2 = new Dictionary<AllowedServices, LocalizedString>();
							for (int l = 0; l < array4.Length; l += 2)
							{
								AllowedServices key = (AllowedServices)this.DeserializeObject((byte[])array4[l], true);
								LocalizedString value = (LocalizedString)this.DeserializeObject((byte[])array4[l + 1], true);
								dictionary2[key] = value;
							}
							obj = dictionary2;
							break;
						}
						}
						break;
					}
				}
				dictionary[rpcOutput] = obj;
			}
			return dictionary;
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x0019705C File Offset: 0x0019525C
		private object DeserializeObject(byte[] data, bool customized)
		{
			if (data != null)
			{
				using (MemoryStream memoryStream = new MemoryStream(data))
				{
					BinaryFormatter binaryFormatter;
					if (customized)
					{
						binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(new CustomizedSerializationBinder());
					}
					else
					{
						binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					}
					return binaryFormatter.Deserialize(memoryStream);
				}
			}
			return null;
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x001970B4 File Offset: 0x001952B4
		private static byte[] SerializeObject(object inputObject)
		{
			if (inputObject != null)
			{
				byte[] result = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					binaryFormatter.Serialize(memoryStream, inputObject);
					result = memoryStream.ToArray();
				}
				return result;
			}
			return null;
		}

		// Token: 0x040035A0 RID: 13728
		private Dictionary<RpcParameters, object> inputParameters;

		// Token: 0x040035A1 RID: 13729
		private Dictionary<RpcOutput, object> outputParameters;
	}
}
