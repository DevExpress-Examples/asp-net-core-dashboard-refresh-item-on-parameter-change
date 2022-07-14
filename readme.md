<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/488203865/21.2.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1091083)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Dashboard for ASP.NET Core - How to Use Parameters to Update a Specific Dashboard Item Without Refreshing the Entire Dashboard

The example shows how to use dashboard parameters to update a specific dashboard item without refreshing the entire dashboard. This technique applies when you need to filter a data source's data to update a specific dashboard item, but do not want to refresh data in all items in a dashboard because this operation causes a performance delay.

The dashboard in the example has three items: 
- The "Products" grid
- The "Sales per Product" chart
- The "Orders" grid

![Dashboard](Images/Dashboard.png)

All items are bound to different data sources. The "Products" grid item [filters](https://docs.devexpress.com/Dashboard/117060/web-dashboard/create-dashboards-on-the-web/interactivity/master-filtering) the "Sales per Product" chart values. 

The selected master filter value is obtained on the client. The [headers](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.AjaxRemoteService#js_devexpress_dashboard_ajaxremoteservice_headers) property is used to pass the value to the server and change the parameter to filter the chart's data source. The [DashboardControl.refresh](https://docs.devexpress.com/Dashboard/js-DevExpress.Dashboard.DashboardControl?p=netframework#js_devexpress_dashboard_dashboardcontrol_refresh) method updates the chart item on the client side.

## Files to Look at

* [Index.cshtml](./CS/AspNetCoreDashboard/Pages/Index.cshtml)
* [_Layout.cshtml](./CS/AspNetCoreDashboard/Pages/_Layout.cshtml#L12-L38)
* [DashboardUtils.cs](./CS/AspNetCoreDashboard/Code/DashboardUtils.cs#L18-L22)

## Documentation

- [Master Filtering](https://docs.devexpress.com/Dashboard/117060/web-dashboard/create-dashboards-on-the-web/interactivity/master-filtering)
- [Manage an In-Memory Data Cache](https://docs.devexpress.com/Dashboard/400983/web-dashboard/dashboard-backend/manage-an-in-memory-data-cache)
- [Dashboard Parameters](https://docs.devexpress.com/Dashboard/117062/web-dashboard/create-dashboards-on-the-web/data-analysis/dashboard-parameters)

