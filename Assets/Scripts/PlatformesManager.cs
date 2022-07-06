using UnityEngine;

public class PlatformesManager : MonoBehaviour
{
    [Tooltip("Platform prefab")]
    [SerializeField]
    private GameObject platformPrefab;

    [Tooltip("Total number of platform to instantiate")]
    [SerializeField]
    private int total_number;

    [Tooltip("Platform Margin")]
    [SerializeField]
    private float platform_margin;

    [SerializeField]
    private float y_offset;

    // min and max offset for the platform positions on the axis
    [SerializeField]
    private float x_offset_min = -2f;
    [SerializeField]
    private float x_offset_max = 2f;


    private float z_offset = 0f;
    private float z_increment = GameCostants.platforms_size + GameCostants.platforms_margin;

    void Start()
    {

        // Instantiate a number of platforms equal to total_number, starting from the second one
        for (int i = 1; i < total_number; i++)
        {
            //randomizing x_offset of the platform
            float x_offset = Random.Range(x_offset_min, x_offset_max);
            //Instantiate the prefab
            Instantiate(platformPrefab, new Vector3(x_offset, y_offset, z_offset + z_increment), Quaternion.identity);
            // update the z_offset
            z_offset += z_increment;
        }
    }

}
