buildPlugin(
  useContainerAgent: true, // Set to `false` if you need to use Docker for containerized tests
  configurations: [
    [platform: 'linux', jdk: 21],
    [platform: 'windows', jdk: 21],
])

pipeline {
  agent {label "Linux"}
  stages {
    stage('Hello') {
      steps {
        echo "hello from Gulsum"
      }
    }
  }     
}