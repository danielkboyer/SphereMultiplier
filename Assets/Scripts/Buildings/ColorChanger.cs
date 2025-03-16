using System.Linq;
using UnityEngine;
#nullable enable
public class ColorChanger : MonoBehaviour
{

    private Material[]? _originalMaterials = null;

    private bool _hasSwitched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ChangeToOriginal()
    {
        if (_originalMaterials == null)
        {
            //We are already original
            return;
        }
        var meshRendererers = GetComponentsInChildren<MeshRenderer>();
        for (var y = 0; y< meshRendererers.Length; y++)
        {
            for (var x = 0; x < meshRendererers[y].materials.Length; x++)
            {

                meshRendererers[y].materials[x] = _originalMaterials[x];
            }
        }
       
    }
    public void ChangeColor(Material material)
    {
        var meshRendererers = GetComponentsInChildren<MeshRenderer>();
        if (!_hasSwitched)
        {
            _hasSwitched = true;
         
            _originalMaterials = meshRendererers.SelectMany(t=>t.materials).ToArray();
        }
        for (var y = 0; y < meshRendererers.Length; y++)
        {
            for (var x = 0; x < meshRendererers[y].materials.Length; x++)
            {

                meshRendererers[y].materials[x] = material;
            }
        }


    }
}
