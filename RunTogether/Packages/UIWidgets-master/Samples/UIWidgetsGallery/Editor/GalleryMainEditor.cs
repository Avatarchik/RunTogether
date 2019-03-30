using UIWidgetsGallery.gallery;
using Unity.UIWidgets.editor;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEditor;
using UnityEngine;

namespace UIWidgetsGallery {
    public class GalleryMainEditor : UIWidgetsEditorWindow {
        
        [MenuItem("UIWidgetsTests/Gallery")]
        public static void gallery() {
            EditorWindow.GetWindow<GalleryMainEditor>();
        }
        
        protected override Widget createWidget() {
            return new GalleryApp();
        }
        
        protected override void Awake() {
            base.Awake();
            FontManager.instance.addFont(Resources.Load<Font>("MaterialIcons-Regular"), "Material Icons");
            FontManager.instance.addFont(Resources.Load<Font>("GalleryIcons"), "GalleryIcons");
        }
    }
}
