using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MyApi.Authorization
{
    public class MyApiAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Customers, L("Customers"));
            context.CreatePermission(PermissionNames.Pages_Products, L("Products"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            SetAdministrativePermission(context);
            SetAppPermission(context);
        }

        private void SetAdministrativePermission(IPermissionDefinitionContext context)
        {

            var userPermission = context.CreatePermission(AdministrativePermissionActions.Administrative_User_See, L("UserSee"),
                L(@"Usuários"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            userPermission.CreateChildPermission(
                   AdministrativePermissionActions.Administrative_User_Edit, L("UserEdit"),
                   L(@"Editar Usuários"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            userPermission.CreateChildPermission(
                  AdministrativePermissionActions.Administrative_User_Get, L("UserGet"),
                  L(@"Get Usuários"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            var customerPermission = context.CreatePermission(AdministrativePermissionActions.Administrative_Customer_See, L("CustomerSee"),
                L(@"Clientes"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            customerPermission.CreateChildPermission(
                AdministrativePermissionActions.Administrative_Customer_Edit, L("ClienteEdit"),
                L(@"Editar Clientes"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            var productsPermission = context.CreatePermission(AdministrativePermissionActions.Administrative_Product_See, L("ProductSee"),
                L(@"Produtos"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);

            productsPermission.CreateChildPermission(AdministrativePermissionActions.Administrative_Product_Edit, L("ProductEdit"),
                L(@"Editar Produtos"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);


        }

        private void SetAppPermission(IPermissionDefinitionContext context)
        {
            context.CreatePermission(AppPermissionNames.App_Home, L("HomeApp"),
                L(@"Home (todos os usuários tem acesso a esse menu)"), multiTenancySides: MultiTenancySides.Host | MultiTenancySides.Tenant);
        }


        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MyApiConsts.LocalizationSourceName);
      
        }
    }
}
