using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C45 RID: 3141
	public abstract class SetWebAppVirtualDirectory<T> : SetExchangeVirtualDirectory<T> where T : ExchangeWebAppVirtualDirectory, new()
	{
		// Token: 0x060076C1 RID: 30401 RVA: 0x001E499A File Offset: 0x001E2B9A
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(IISNotInstalledException).IsInstanceOfType(exception);
		}

		// Token: 0x17002497 RID: 9367
		// (get) Token: 0x060076C2 RID: 30402 RVA: 0x001E49B7 File Offset: 0x001E2BB7
		// (set) Token: 0x060076C3 RID: 30403 RVA: 0x001E49CE File Offset: 0x001E2BCE
		[Parameter]
		public bool BasicAuthentication
		{
			get
			{
				return (bool)base.Fields["BasicAuthentication"];
			}
			set
			{
				base.Fields["BasicAuthentication"] = value;
			}
		}

		// Token: 0x17002498 RID: 9368
		// (get) Token: 0x060076C4 RID: 30404 RVA: 0x001E49E6 File Offset: 0x001E2BE6
		// (set) Token: 0x060076C5 RID: 30405 RVA: 0x001E49FD File Offset: 0x001E2BFD
		[Parameter]
		public bool WindowsAuthentication
		{
			get
			{
				return (bool)base.Fields["WindowsAuthentication"];
			}
			set
			{
				base.Fields["WindowsAuthentication"] = value;
			}
		}

		// Token: 0x17002499 RID: 9369
		// (get) Token: 0x060076C6 RID: 30406 RVA: 0x001E4A15 File Offset: 0x001E2C15
		// (set) Token: 0x060076C7 RID: 30407 RVA: 0x001E4A2C File Offset: 0x001E2C2C
		[Parameter]
		public bool LiveIdAuthentication
		{
			get
			{
				return (bool)base.Fields["LiveIdAuthentication"];
			}
			set
			{
				base.Fields["LiveIdAuthentication"] = value;
			}
		}

		// Token: 0x1700249A RID: 9370
		// (get) Token: 0x060076C8 RID: 30408 RVA: 0x001E4A44 File Offset: 0x001E2C44
		// (set) Token: 0x060076C9 RID: 30409 RVA: 0x001E4A5B File Offset: 0x001E2C5B
		[Parameter]
		public GzipLevel GzipLevel
		{
			get
			{
				return (GzipLevel)base.Fields["GzipLevel"];
			}
			set
			{
				base.Fields["GzipLevel"] = value;
			}
		}

		// Token: 0x1700249B RID: 9371
		// (get) Token: 0x060076CA RID: 30410 RVA: 0x001E4A73 File Offset: 0x001E2C73
		// (set) Token: 0x060076CB RID: 30411 RVA: 0x001E4A8A File Offset: 0x001E2C8A
		protected bool FormsAuthentication
		{
			get
			{
				return (bool)base.Fields["FormsAuthentication"];
			}
			set
			{
				base.Fields["FormsAuthentication"] = value;
			}
		}

		// Token: 0x1700249C RID: 9372
		// (get) Token: 0x060076CC RID: 30412 RVA: 0x001E4AA2 File Offset: 0x001E2CA2
		// (set) Token: 0x060076CD RID: 30413 RVA: 0x001E4AB9 File Offset: 0x001E2CB9
		protected bool DigestAuthentication
		{
			get
			{
				return (bool)base.Fields["DigestAuthentication"];
			}
			set
			{
				base.Fields["DigestAuthentication"] = value;
			}
		}

		// Token: 0x1700249D RID: 9373
		// (get) Token: 0x060076CE RID: 30414 RVA: 0x001E4AD1 File Offset: 0x001E2CD1
		// (set) Token: 0x060076CF RID: 30415 RVA: 0x001E4AE8 File Offset: 0x001E2CE8
		protected bool AdfsAuthentication
		{
			get
			{
				return (bool)base.Fields["AdfsAuthentication"];
			}
			set
			{
				base.Fields["AdfsAuthentication"] = value;
			}
		}

		// Token: 0x1700249E RID: 9374
		// (get) Token: 0x060076D0 RID: 30416 RVA: 0x001E4B00 File Offset: 0x001E2D00
		// (set) Token: 0x060076D1 RID: 30417 RVA: 0x001E4B17 File Offset: 0x001E2D17
		protected bool OAuthAuthentication
		{
			get
			{
				return (bool)base.Fields["OAuthAuthentication"];
			}
			set
			{
				base.Fields["OAuthAuthentication"] = value;
			}
		}

		// Token: 0x1700249F RID: 9375
		// (get) Token: 0x060076D2 RID: 30418 RVA: 0x001E4B2F File Offset: 0x001E2D2F
		// (set) Token: 0x060076D3 RID: 30419 RVA: 0x001E4B37 File Offset: 0x001E2D37
		internal new MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				return base.InternalAuthenticationMethods;
			}
			set
			{
				base.InternalAuthenticationMethods = value;
			}
		}

		// Token: 0x060076D4 RID: 30420 RVA: 0x001E4B40 File Offset: 0x001E2D40
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.Fields.Contains("WindowsAuthentication"))
			{
				T dataObject = this.DataObject;
				if (dataObject.WindowsAuthentication)
				{
					goto IL_56;
				}
			}
			if (!base.Fields.Contains("DigestAuthentication"))
			{
				return;
			}
			T dataObject2 = this.DataObject;
			if (!dataObject2.DigestAuthentication)
			{
				return;
			}
			IL_56:
			if (base.Fields.Contains("WindowsAuthentication"))
			{
				T dataObject3 = this.DataObject;
				if (dataObject3.FormsAuthentication)
				{
					T dataObject4 = this.DataObject;
					dataObject4.FormsAuthentication = false;
					this.FormsAuthentication = false;
				}
			}
		}

		// Token: 0x060076D5 RID: 30421 RVA: 0x001E4BE7 File Offset: 0x001E2DE7
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			WebAppVirtualDirectoryHelper.UpdateFromMetabase((ExchangeWebAppVirtualDirectory)dataObject);
			dataObject.ResetChangeTracking();
			base.StampChangesOn(dataObject);
		}

		// Token: 0x060076D6 RID: 30422 RVA: 0x001E4C04 File Offset: 0x001E2E04
		protected virtual void UpdateDataObject(T dataObject)
		{
			if ((base.Fields.Contains("FormsAuthentication") && this.FormsAuthentication != dataObject.FormsAuthentication) || (base.Fields.Contains("LiveIdAuthentication") && this.LiveIdAuthentication != dataObject.LiveIdAuthentication) || (base.Fields.Contains("AdfsAuthentication") && this.AdfsAuthentication != dataObject.AdfsAuthentication) || (base.Fields.Contains("OAuthAuthentication") && this.OAuthAuthentication != dataObject.OAuthAuthentication) || (base.Fields.Contains("GzipLevel") && this.GzipLevel != dataObject.GzipLevel) || dataObject.IsChanged(ADOwaVirtualDirectorySchema.LogonFormat) || dataObject.IsChanged(ExchangeWebAppVirtualDirectorySchema.FormsAuthentication))
			{
				this.WriteWarning(Strings.NeedIisRestartWarning);
			}
		}

		// Token: 0x060076D7 RID: 30423 RVA: 0x001E4D10 File Offset: 0x001E2F10
		protected sealed override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			T t = (T)((object)base.PrepareDataObject());
			if (base.HasErrors)
			{
				return null;
			}
			this.UpdateDataObject(t);
			bool flag = base.Fields.Contains("FormsAuthentication");
			bool flag2 = false;
			if (flag)
			{
				flag2 = this.FormsAuthentication;
			}
			if (flag && !flag2)
			{
				t.FormsAuthentication = false;
			}
			if (base.Fields.Contains("BasicAuthentication"))
			{
				t.BasicAuthentication = this.BasicAuthentication;
			}
			if (base.Fields.Contains("DigestAuthentication"))
			{
				t.DigestAuthentication = this.DigestAuthentication;
			}
			if (base.Fields.Contains("WindowsAuthentication"))
			{
				t.WindowsAuthentication = this.WindowsAuthentication;
			}
			if (base.Fields.Contains("LiveIdAuthentication"))
			{
				t.LiveIdAuthentication = this.LiveIdAuthentication;
			}
			if (base.Fields.Contains("AdfsAuthentication"))
			{
				t.AdfsAuthentication = this.AdfsAuthentication;
			}
			if (base.Fields.Contains("OAuthAuthentication"))
			{
				t.OAuthAuthentication = this.OAuthAuthentication;
			}
			if (flag && flag2)
			{
				t.FormsAuthentication = true;
			}
			if (!t.BasicAuthentication && !t.DigestAuthentication && !t.WindowsAuthentication && !t.FormsAuthentication && !t.LiveIdAuthentication && !t.AdfsAuthentication && !t.OAuthAuthentication)
			{
				this.WriteWarning(Strings.NoAuthenticationWarning(this.Identity.ToString(), this.CmdletName));
			}
			TaskLogger.LogExit();
			return t;
		}

		// Token: 0x170024A0 RID: 9376
		// (get) Token: 0x060076D8 RID: 30424 RVA: 0x001E4EF4 File Offset: 0x001E30F4
		private string CmdletName
		{
			get
			{
				object[] customAttributes = base.GetType().GetCustomAttributes(typeof(CmdletAttribute), false);
				CmdletAttribute cmdletAttribute = (CmdletAttribute)customAttributes[0];
				return cmdletAttribute.VerbName + "-" + cmdletAttribute.NounName;
			}
		}

		// Token: 0x060076D9 RID: 30425 RVA: 0x001E4F37 File Offset: 0x001E3137
		internal void CheckGzipLevelIsNotError(GzipLevel gzipLevel)
		{
			if (gzipLevel == GzipLevel.Error)
			{
				base.WriteError(new TaskException(Strings.GzipCannotBeSetToError), ErrorCategory.NotSpecified, null);
			}
		}
	}
}
