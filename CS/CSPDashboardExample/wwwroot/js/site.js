function onBeforeRender(dashboardControl) {
    dashboardControl.registerExtension(new DevExpress.Dashboard.DashboardPanelExtension(dashboardControl, { dashboardThumbnail: "./DashboardThumbnail/{0}.png" }));
}